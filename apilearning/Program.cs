using apilearning.MyDbContext;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace apilearning
{

    /// <summary>
    /// api�汾ö��
    /// </summary>
    public enum APIVersions
    {
        /// <summary>
        /// V1�汾
        /// </summary>
        v1,
        /// <summary>
        /// V2�汾
        /// </summary>
        v2
    }
    public class Program
    {
        public static string Area { get; } = "Home";
        public static void Main(string[] args) {
            // 1. ��������
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.  ��������ӷ���

            // ���api������
            builder.Services.AddControllers(setup =>
            {
                // ��� ������������� �� application/json ����֧��json���� �򷵻�406״̬�룬false �ͷ��ط�����֧�ֵ�����
                setup.ReturnHttpNotAcceptable = true;
                //�������ݸ�ʽ������ Ĭ��ֻ֧��json,�������xml�����ݸ�ʽ����
                setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()); //����xml

            })
                //.AddXmlDataContractSerializerFormatters() // ���ַ�ʽҲ�����xml��ʽ����,����xml
                .AddNewtonsoftJson(options =>
                {
                    // ���ʱ���ʽ������
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // ���AutoMapper����
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            // ��ӿ���֧��
            builder.Services.AddCors(setUp =>
            {
                setUp.AddPolicy("cor", builder => builder.AllowAnyOrigin());
            });

            builder.Services.AddSwaggerGen(options =>
            {
                // ��ȡxml ����
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // ����ע�� ���ڶ������� ���Ƿ���ʾ������ע��
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);


                // api�汾����
                typeof(APIVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    //����ĵ�����
                    options.SwaggerDoc(version, new OpenApiInfo
                    {
                        Title = "webapi��ϰ",
                        Version = version,
                        Description = $"��Ŀ��:{version}�汾"
                    });
                });

            });
            // ע�����ݿ������� home
            builder.Services.AddDbContext<accountContext>(opt =>
            {
                opt.UseMySql(builder.Configuration.GetConnectionString("Home"), ServerVersion.Parse("8.0.28-mysql"));
            });

            //// ע�����ݿ�������office
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
                // �汾�л�
                app.UseSwaggerUI(options =>
                {

                    /*
                     options.SwaggerEndpoint($"/swagger/V1/swagger.json",$"�汾ѡ��:V1");
                    */
                    //���ֻ��һ���汾ҲҪ���Ϸ�����һ��
                    typeof(APIVersions).GetEnumNames().ToList().ForEach(version =>
                    {
                        //�л��汾����
                        //����һ��ʹ�õ��ĸ�json�ļ�,���������Ǹ�����
                        options.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"�汾ѡ��:{version}");
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