using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TransferAppCQRS.Domain.CommandHandlers;
using TransferAppCQRS.Domain.Commands;
using TransferAppCQRS.Domain.Core.Bus;
using TransferAppCQRS.Domain.Core.Events;
using TransferAppCQRS.Domain.Core.Notifications;
using TransferAppCQRS.Domain.EventHandlers;
using TransferAppCQRS.Domain.Events;
using TransferAppCQRS.Domain.Interfaces;
using TransferAppCQRS.Domain.ModelsNoSql.Contracts;
using TransferAppCQRS.Infra.CrossCutting.Bus;
using TransferAppCQRS.Infra.CrossCutting.QueueManager;
using TransferAppCQRS.Infra.Data.DomainRepositories;
using TransferAppCQRS.Infra.Data.EventSourcingRepository;
using TransferAppCQRS.Infra.Data.NoSql.Repositories;
using TransferAppCQRS.Infra.Data.UoW;

namespace TransferAppCQRS.Infra.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<IQueueManager, QueueManager>();

            // ASP.NET Authorization Polices
            //services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>(); ;

            //services.AddSingleton(Mapper.Configuration);
            //  Inserido no startup: services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
            //  Inserido no startup: services.AddScoped<ICustomerAppService, CustomerAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<AccountRegisteredEvent>, AccountEventHandler>();

            // Domain - Commands
            services.AddScoped<INotificationHandler<RegisterNewCustomerCommand>, CustomerCommandHandler>();
            services.AddScoped<INotificationHandler<RegisterNewAccountCommand>, AccountCommandHandler>();
            services.AddScoped<INotificationHandler<RegisterNewTransferCommand>, TransferCommandHandler>();
            

            // Infra - Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICustomerReadNoSql, CusomerReadRepository>();
            services.AddScoped<ICustomerWriteNoSqlRepository, CustomerWriteRepository>();

            //services.AddScoped<EquinoxContext>();

            // Infra - Data EventSourcing
            //services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();            
            //services.AddScoped<IEventStore, SqlEventStore>();
            //services.AddScoped<EventStoreSQLContext>();
            services.AddScoped<IEventStore, EventRepository>();


            // Infra - Identity Services
            //services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            //services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // Infra - Identity
            //services.AddScoped<IUser, AspNetUser>();
        }
    }
}
