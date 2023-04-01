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
    /*El patrón de inyección de dependencias es un patrón de diseño de software
     * que se utiliza para lograr una separación más clara entre los componentes
     * de una aplicación y para facilitar el mantenimiento y la prueba de la aplicación.
     * En lugar de que los componentes de una aplicación creen y gestionen sus propias 
     * dependencias, el patrón de inyección de dependencias permite que estas dependencias
     * sean "inyectadas" en los componentes desde el exterior.En la práctica, esto significa
     * que los componentes no tienen que preocuparse por crear o gestionar sus dependencias,
     * lo que puede simplificar su código y hacerlo más fácil de mantener y probar. En cambio,
     * las dependencias se proporcionan a los componentes cuando se crean o se inicializan,
     * ya sea mediante una llamada explícita o mediante un mecanismo de autoinicialización.
     * El patrón de inyección de dependencias también puede permitir que los componentes 
     * se intercambien fácilmente y se prueben de manera aislada, lo que puede ser especialmente
     * En resumen, el patrón de inyección de dependencias es un patrón de diseño de software 
     * que se utiliza para lograr una separación más clara entre los componentes de una aplicación
     * y para facilitar el mantenimiento y la prueba de la aplicación mediante la "inyección" 
     * de dependencias desde el exterior.*/
    #region Servicios
    services.AddScoped<IDbConnection>(x => new SqlConnection(builder.Configuration.GetConnectionString("AppContext")));
    services.AddScoped<IUsuarioRepository, UsersRepository>();
    services.AddScoped<IRolesRepository, RolesRepository>();
    services.AddScoped<IUserRolesRepository, UserRolesRepository>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    #endregion
    /*El patrón Mediator es un patrón de diseño de software que se utiliza para
     * reducir la complejidad y mejorar la modularidad de las aplicaciones que 
     * tienen muchos objetos interconectados y que necesitan comunicarse entre 
     * sí de manera eficiente.En el patrón Mediator, un objeto llamado Mediator
     * actúa como intermediario entre los objetos que necesitan comunicarse.
     * Los objetos no se comunican directamente entre sí, sino que lo hacen 
     * a través del Mediator.El Mediator se encarga de gestionar las comunicaciones
     * entre los objetos y de distribuir la información que reciben a los objetos
     * correspondientes. Esto significa que cada objeto sólo tiene que conocer al Mediator 
     * y no tiene que preocuparse por conocer los detalles de los demás objetos con los que se comunica.*/
    #region MediatR
    services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());
    #endregion
    /*AutoMapper es una biblioteca de mapeo de objetos en .NET que permite simplificar la conversión de objetos de un tipo a otro.
     * En lugar de escribir código personalizado para asignar cada propiedad de un objeto de origen a su correspondiente en un objeto
     * de destino, AutoMapper utiliza convenciones y configuraciones para realizar estas asignaciones automáticamente.
     * AutoMapper también puede manejar relaciones complejas entre objetos y proporciona características adicionales, 
     * como la posibilidad de ignorar o personalizar las asignaciones para casos específicos.
     * En general, AutoMapper puede ahorrar tiempo y reducir la cantidad de código necesario para mapear objetos de un tipo a otro en una aplicación .NET.
     * Es especialmente útil en aplicaciones que utilizan patrones de arquitectura de software como MVC o MVVM,
     * donde los objetos de la capa de modelo pueden requerir asignaciones frecuentes a objetos de la capa de presentación o viceversa.*/
    #region AutoMapper
    builder.Services.AddAutoMapper(typeof(Program));
    #endregion
    /*NLog es una biblioteca de registro (logging) de código abierto para la plataforma .NET que permite a los desarrolladores registrar mensajes de registro en sus aplicaciones.
     * Con NLog, los desarrolladores pueden crear configuraciones de registro personalizadas para sus aplicaciones, 
     * lo que les permite controlar cómo se registran los mensajes de registro y dónde se almacenan. NLog es altamente configurable
     * y puede utilizarse con varios destinos de registro, incluyendo archivos de registro, bases de datos,
     * servicios de correo electrónico y servicios de transmisión en tiempo real.NLog también es altamente escalable
     * y puede utilizarse en aplicaciones de cualquier tamaño. Además, ofrece una amplia gama de características avanzadas,
     * como la configuración de múltiples objetivos de registro, la filtración de mensajes de registro 
     * y la internacionalización de los mensajes de registro.*/
    #region NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LogBehaviour<,>));
    #endregion
    /*Identity Core es un marco de autenticación y autorización para aplicaciones web basadas en ASP.NET.
     * Proporciona una manera fácil de agregar autenticación y autorización a una aplicación web, incluyendo
     * características como registro de usuarios, inicio de sesión, recuperación de contraseña, 
     * roles y políticas de autorización.Identity Core está diseñado para ser flexible y extensible,
     * lo que significa que se puede personalizar para adaptarse a las necesidades específicas
     * de la aplicación. También es compatible con múltiples proveedores de almacenamiento de datos,
     * como bases de datos SQL, NoSQL y en memoria, lo que permite que la aplicación elija el proveedor
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

