using MikeRosoft.Controllers;
using MikeRosoft.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MikeRosoft.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MikeRosoft.UT;
using MikeRosoft.Models.OrderViewModels;
using System.Collections;

namespace MikeRosoft.UT.Controllers.BuyProductController_test
{
    class BuyProduct_test_createGET
    {

        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("MikeRosoft")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }


        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;
        Microsoft.AspNetCore.Http.DefaultHttpContext ordersContext;






    }
}
