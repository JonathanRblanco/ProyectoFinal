using ManejoPresupuestos.Servicios;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Data.SqlClient;
using NLog;
using NLog.Web;
using ProyectoFinal.Contracts;
using ProyectoFinal.Loggin;
using ProyectoFinal.Models;
using ProyectoFinal.Repositorys;
using ProyectoFinal.Stores;
using ProyectoFinal.UnitOfWork;
using System.Data;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);
    // Add services to the container.
    var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    builder.Services.AddControllersWithViews(opciones =>
    {
        opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
    });
    var services = builder.Services;
    /*El patr�n de inyecci�n de dependencias es un patr�n de dise�o de software
     * que se utiliza para lograr una separaci�n m�s clara entre los componentes
     * de una aplicaci�n y para facilitar el mantenimiento y la prueba de la aplicaci�n.
     * En lugar de que los componentes de una aplicaci�n creen y gestionen sus propias 
     * dependencias, el patr�n de inyecci�n de dependencias permite que estas dependencias
     * sean "inyectadas" en los componentes desde el exterior.En la pr�ctica, esto significa
     * que los componentes no tienen que preocuparse por crear o gestionar sus dependencias,
     * lo que puede simplificar su c�digo y hacerlo m�s f�cil de mantener y probar. En cambio,
     * las dependencias se proporcionan a los componentes cuando se crean o se inicializan,
     * ya sea mediante una llamada expl�cita o mediante un mecanismo de autoinicializaci�n.
     * El patr�n de inyecci�n de dependencias tambi�n puede permitir que los componentes 
     * se intercambien f�cilmente y se prueben de manera aislada, lo que puede ser especialmente
     * En resumen, el patr�n de inyecci�n de dependencias es un patr�n de dise�o de software 
     * que se utiliza para lograr una separaci�n m�s clara entre los componentes de una aplicaci�n
     * y para facilitar el mantenimiento y la prueba de la aplicaci�n mediante la "inyecci�n" 
     * de dependencias desde el exterior.*/
    #region Servicios
    services.AddScoped<IDbConnection>(x => new SqlConnection(builder.Configuration.GetConnectionString("AppContext")));
    services.AddScoped<IUsuarioRepository, UsersRepository>();
    services.AddScoped<IRolesRepository, RolesRepository>();
    services.AddScoped<IUserRolesRepository, UserRolesRepository>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    #endregion
    /*El patr�n Mediator es un patr�n de dise�o de software que se utiliza para
     * reducir la complejidad y mejorar la modularidad de las aplicaciones que 
     * tienen muchos objetos interconectados y que necesitan comunicarse entre 
     * s� de manera eficiente.En el patr�n Mediator, un objeto llamado Mediator
     * act�a como intermediario entre los objetos que necesitan comunicarse.
     * Los objetos no se comunican directamente entre s�, sino que lo hacen 
     * a trav�s del Mediator.El Mediator se encarga de gestionar las comunicaciones
     * entre los objetos y de distribuir la informaci�n que reciben a los objetos
     * correspondientes. Esto significa que cada objeto s�lo tiene que conocer al Mediator 
     * y no tiene que preocuparse por conocer los detalles de los dem�s objetos con los que se comunica.*/
    #region MediatR
    services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());
    #endregion
    /*AutoMapper es una biblioteca de mapeo de objetos en .NET que permite simplificar la conversi�n de objetos de un tipo a otro.
     * En lugar de escribir c�digo personalizado para asignar cada propiedad de un objeto de origen a su correspondiente en un objeto
     * de destino, AutoMapper utiliza convenciones y configuraciones para realizar estas asignaciones autom�ticamente.
     * AutoMapper tambi�n puede manejar relaciones complejas entre objetos y proporciona caracter�sticas adicionales, 
     * como la posibilidad de ignorar o personalizar las asignaciones para casos espec�ficos.
     * En general, AutoMapper puede ahorrar tiempo y reducir la cantidad de c�digo necesario para mapear objetos de un tipo a otro en una aplicaci�n .NET.
     * Es especialmente �til en aplicaciones que utilizan patrones de arquitectura de software como MVC o MVVM,
     * donde los objetos de la capa de modelo pueden requerir asignaciones frecuentes a objetos de la capa de presentaci�n o viceversa.*/
    #region AutoMapper
    builder.Services.AddAutoMapper(typeof(Program));
    #endregion
    /*NLog es una biblioteca de registro (logging) de c�digo abierto para la plataforma .NET que permite a los desarrolladores registrar mensajes de registro en sus aplicaciones.
     * Con NLog, los desarrolladores pueden crear configuraciones de registro personalizadas para sus aplicaciones, 
     * lo que les permite controlar c�mo se registran los mensajes de registro y d�nde se almacenan. NLog es altamente configurable
     * y puede utilizarse con varios destinos de registro, incluyendo archivos de registro, bases de datos,
     * servicios de correo electr�nico y servicios de transmisi�n en tiempo real.NLog tambi�n es altamente escalable
     * y puede utilizarse en aplicaciones de cualquier tama�o. Adem�s, ofrece una amplia gama de caracter�sticas avanzadas,
     * como la configuraci�n de m�ltiples objetivos de registro, la filtraci�n de mensajes de registro 
     * y la internacionalizaci�n de los mensajes de registro.*/
    #region NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LogBehaviour<,>));
    #endregion
    /*Identity Core es un marco de autenticaci�n y autorizaci�n para aplicaciones web basadas en ASP.NET.
     * Proporciona una manera f�cil de agregar autenticaci�n y autorizaci�n a una aplicaci�n web, incluyendo
     * caracter�sticas como registro de usuarios, inicio de sesi�n, recuperaci�n de contrase�a, 
     * roles y pol�ticas de autorizaci�n.Identity Core est� dise�ado para ser flexible y extensible,
     * lo que significa que se puede personalizar para adaptarse a las necesidades espec�ficas
     * de la aplicaci�n. Tambi�n es compatible con m�ltiples proveedores de almacenamiento de datos,
     * como bases de datos SQL, NoSQL y en memoria, lo que permite que la aplicaci�n elija el proveedor
     * de almacenamiento de datos que mejor se adapte a sus necesidades.*/
    #region Identity
    services.AddScoped<IUserStore<User>, UserStore>();
    services.AddScoped<IRoleStore<IdentityRole>, RoleStore>();
    services.AddIdentity<User, IdentityRole>()
        .AddDefaultTokenProviders()
        .AddErrorDescriber<MensajesDeErrorIdentity>();

    services.Configure<IdentityOptions>(options =>
    {
        // Password settings
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.AllowedForNewUsers = false;

        // User settings
        options.User.AllowedUserNameCharacters =
          "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;
    });
    services.ConfigureApplicationCookie(options =>
    {
        // Cookie settings
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.SlidingExpiration = true;
    });
    #endregion
    var app = builder.Build();
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    logger.Error("Error : {0} {1}", ex.Message, ex.StackTrace);
    throw;
}
finally
{
    logger.Info("La aplicacion se ha detenido");
    LogManager.Shutdown();
}

