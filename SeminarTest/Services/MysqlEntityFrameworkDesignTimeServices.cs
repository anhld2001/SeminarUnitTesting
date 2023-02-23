using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using MySql.EntityFrameworkCore.Extensions;

namespace SeminarTest.Service
{
    public class MysqlEntityFrameworkDesignTimeServices : IDesignTimeServices
    {
        public MysqlEntityFrameworkDesignTimeServices()
        {
        }

        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddEntityFrameworkMySQL();
            new EntityFrameworkRelationalDesignServicesBuilder(serviceCollection)
                .TryAddCoreServices();
        }
    }
}
