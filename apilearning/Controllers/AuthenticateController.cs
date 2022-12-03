using apilearning.ModelDtos;
using apilearning.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace apilearning.Controllers
{
    /// <summary>
    /// 生成token
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("/api/[controller]")]
    public class AuthenticateController : CommonController
    {
        private readonly IConfiguration _configuration;
        public AuthenticateController(IConfiguration configuration) {
            _configuration = configuration;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginDto">登录接收的内容</param>
        /// <returns></returns>
        [AllowAnonymous] // 允许任意用户
        [HttpPost("login")]
        public ActionResult<ApiResult<string>> Login([FromBody] LoginDto loginDto) {
            if(string.IsNullOrWhiteSpace(loginDto.Username) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                return BadRequest("用户名或密码不能为空");
            }
            // 生成token
            var token = GenerateJwt();
            var t = CommonResponse(token, message: "请求成功", ApiResultCode.Success);
            return Ok(t);
        }

        /// <summary>
        /// 生成token
        /// </summary>
        /// <returns></returns>
        private string GenerateJwt() {
            // 1. 选择加密算法
            var algorithm = SecurityAlgorithms.HmacSha256;

            // 2. 定义claim携带的内容 就是palyload
            var claims = new[] { 
                // sub 用户id
                new Claim(JwtRegisteredClaimNames.Sub,"zaks"),
                //角色
                new Claim(ClaimTypes.Role,"admin")
            };

            // 从appsettings里面读取SecretKey 并加密 下面是拆分的步骤
            //           var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SrcretKey"]));

            //3. 获取secrityKey
            var secrityKey = _configuration["Authentication:SecretKey"];
            // 4. 加密
            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrityKey));
            //5.验证加密
            var checkSignKey = new SigningCredentials(signKey, algorithm);

            var token = new JwtSecurityToken(
                // token签发者 可以是域名
                issuer: _configuration["Authentication:Issuer"],
                //token发布给谁 ,可以省略，可以同isuser一样
                audience: _configuration["Authentication:Audience"],
               claims,//token负载的内容
                      //发布时间
               notBefore: DateTime.UtcNow,
               //有效期一天
               expires: DateTime.UtcNow.AddDays(1),
               signingCredentials: checkSignKey
                );
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenStr;


        }
    }
}
