using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using Dapper;
using System.Data;

namespace CineNet.Infraestructure.Repositories
{
    public class RoomsRepository : IRoomsRepository
    {
        private readonly IDbConnection _connection;

        public RoomsRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<IEnumerable<Room>> GetAllAsync(IDbTransaction transaction)
        {
            return await _connection.QueryAsync<Room, Branch, Cinema, Room>(getAllRoomsQuery,
                (room, cinemaBranch, cinema) =>
                {
                    cinemaBranch.Cinema = cinema;
                    room.CinemaBranch = cinemaBranch;

                    return room;
                }
                , transaction, splitOn: "Id");
        }
        public async Task<int> Create(Room room, IDbTransaction transaction)
        {
            return await _connection.ExecuteScalarAsync<int>(createRoomQuery, room, transaction);
        }

        public async Task<Room> GetById(int id, IDbTransaction transaction)
        {
            var room = await _connection.QueryAsync<Room, Branch, Cinema, Room>(getRoomByIdQuery,
                (room, cinemaBranch, cinema) =>
                {
                    cinemaBranch.Cinema = cinema;
                    room.CinemaBranch = cinemaBranch;

                    return room;
                }
                , new { Id = id }, transaction, splitOn: "Id");
            return room.FirstOrDefault();
        }

        public async Task Update(Room room, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(updateRoomByIdQuery, room, transaction);
        }

        public async Task Delete(int id, IDbTransaction transaction)
        {
            await _connection.ExecuteAsync(deleteRoomQuery, new { Id = id }, transaction);
        }

        private const string getAllRoomsQuery = @"SELECT r.Id,r.Number,r.Capacity,r.CinemaBranchId,cb.Id,cb.CineId,cb.Name,cb.Address,cb.Location,cb.Province,cb.CP,cb.Email,cb.Phone,c.Id,c.Name,c.URL,c.Status
                                                FROM Rooms r
                                                INNER JOIN CinemaBranchs cb ON cb.Id = r.CinemaBranchId
                                                INNER JOIN Cinemas c on c.Id = cb.CineId";
        private const string createRoomQuery = @"INSERT INTO Rooms
                                                VALUES(@Number,@Capacity,@CinemaBranchId);
                                                SELECT CAST(SCOPE_IDENTITY() AS int)";
        private const string getRoomByIdQuery = @"SELECT r.Id,r.Number,r.Capacity,r.CinemaBranchId,cb.Id,cb.CineId,cb.Name,cb.Address,cb.Location,cb.Province,cb.CP,cb.Email,cb.Phone,c.Id,c.Name,c.URL,c.Status
                                                FROM Rooms r
                                                INNER JOIN CinemaBranchs cb ON cb.Id = r.CinemaBranchId
                                                INNER JOIN Cinemas c on c.Id = cb.CineId
                                                WHERE r.Id = @Id";
        private const string updateRoomByIdQuery = @"UPDATE Rooms
                                                        SET Number = @Number,Capacity = @Capacity,CinemaBranchId = @CinemaBranchId
                                                        WHERE Id = @Id";
        private const string deleteRoomQuery = @"DELETE FROM Rooms
                                                WHERE Id = @Id";
    }
}
