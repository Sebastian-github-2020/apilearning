using apilearning.MyDbContext;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace apilearning
{

    /// <summary>
    /// api版本枚举
    /// </summary>
    public enum APIVersions
    {
        /// <summary>
        /// V1版本
        /// </summary>
        v1,
        /// <summary>
        /// V2版本
        /// </summary>
        v2
    }
    public class Program
    {
        public static string Area { get; } = "Home";
        public static void Main(string[] args) {
            // 1. 创建容器
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.  向容器添加服务

            // 添加api控制器
            builder.Services.AddControllers(setup =>
            {
                // 如果 请求的数据类型 如 application/json 服务不支持json类型 则返回406状态码，false 就返回服务器支持的类型
                setup.ReturnHttpNotAcceptable = true;
                //配置数据格式化器， 默认只支持json,这里添加xml的数据格式化器
                setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()); //优先xml

            })
                //.AddXmlDataContractSerializerFormatters() // 这种方式也能添加xml格式化器,优先xml
                .AddNewtonsoftJson(options =>
                {
                    // 添加时间格式化服务
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // 添加AutoMapper服务
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // 添加跨域支持
            builder.Services.AddCors(setUp =>
            {
                setUp.AddPolicy("cor", builder => builder.AllowAnyOrigin());
            });

            builder.Services.AddSwaggerGen(options =>
            {
                // 获取xml 名称
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // 包含注释 ，第二个参数 ：是否显示控制器注释
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);


                // api版本控制
                typeof(APIVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    //添加文档介绍
                    options.SwaggerDoc(version, new OpenApiInfo
                    {
                        Title = "webapi练习",
                        Version = version,
                        Description = $"项目名:{version}版本"
                    });
                });

            });
            // 注册数据库上下文 home
            builder.Services.AddDbContext<accountContext>(opt =>
            {
                opt.UseMySql(builder.Configuration.GetConnectionString("Home"), ServerVersion.Parse("8.0.28-mysql"));
            });

            //// 注册数据库上下文office
            builder.Services.AddDbContext<netsqlContext>(opt =>
            {
                opt.UseMySql(builder.Configuration.GetConnectionString("Office"), ServerVersion.Parse("8.0.29-mysql"));
            });







            var app = builder.Build();

            app.UseCors("cor");
            // Configure the HTTP request pipeline.
            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                // 版本切换
                app.UseSwaggerUI(options =>
                {

                    /*
                     options.SwaggerEndpoint($"/swagger/V1/swagger.json",$"版本选择:V1");
                    */
                    //如果只有一个版本也要和上方保持一致
                    typeof(APIVersions).GetEnumNames().ToList().ForEach(version =>
                    {
                        //切换版本操作
                        //参数一是使用的哪个json文件,参数二就是个名字
                        options.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"版本选择:{version}");
                    });
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}