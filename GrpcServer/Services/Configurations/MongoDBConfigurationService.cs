using System.Collections.Generic;
using GrpcServer.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GrpcServer.Services.Configurations
{
    public static class MongoDBConfigurationService
    {
        public static IServiceCollection MongoDBConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //Dependency injection part
            services.AddSingleton<IMongoDBContext, MongoDBContext>();
            services.AddSingleton<IMongoSettings, MongoSettings>();

            //Configuration
            services.Configure<MongoDBModel>(options =>
            {
                options.ConnectionString = configuration.GetSection("mongodb:connectionString").Value;
                options.Database = configuration.GetSection("mongodb:database").Value;
                options.Collections = configuration.GetSection("mongodb:collections").Get<Dictionary<string, string>>();
            });

            return services;
        }

        /// <summary>
        ///  IF some of MongoDB configuration are added here then the client must have
        ///  and know about this configurations. This is why it's better if this
        ///  configuration name space must be in a global class library, in this case in 
        ///  TestGrpc.
        /// </summary>
        public class MongoSettings : IMongoSettings
        {
            public void ConfigureMongodb()
            {
                // var pack = new ConventionPack();
                // var regex = new Regex(
                //     @"(?<=[A-Z])(?=[A-Z][a-z]) 
                //     |(?<=[^A-Z])(?=[A-Z]) 
                //     |(?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
                // pack.AddMemberMapConvention("snakecase", m => m.SetElementName(regex.Replace(m.ElementName, "_").ToLower()));
                // ConventionRegistry.Register("snakecase", pack, type => true);

                // var enumConvention = new ConventionPack { new EnumRepresentationConvention(BsonType.String) };
                // ConventionRegistry.Register("EnumToString", enumConvention, type => true);
                
                // //Base Model ClassMap
                // BsonClassMap.RegisterClassMap<Base>(cm =>
                // {
                //     cm.AutoMap();
                //     foreach (var member in RuntimeTypeModel.Default[typeof(Base)].GetFields())
                //     {
                //         cm.MapMember(typeof(Base).GetMember(member.Member.Name)[0])
                //             .SetElementName(member.FieldNumber.ToString())
                //             .SetOrder(member.FieldNumber);
                //     }
                // });
            }
        }
    }

    public interface IMongoSettings
    {
        void ConfigureMongodb();
    }
}