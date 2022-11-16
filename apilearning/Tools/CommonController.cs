using Microsoft.AspNetCore.Mvc;

namespace apilearning.Tools {
    /// <summary>
    /// 封装一个通用的ControllerBase
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CommonController : ControllerBase {
        /// <summary>
        /// 利用这个方法创建统一的返回格式
        /// </summary>
        /// <typeparam name="T">需要返回的数据类型</typeparam>
        /// <param name="datas">接口需要返回的数据</param>
        /// <param name="message"></param>
        /// <param name="code">返回的状态码</param>
        /// <param name="ex">异常信息</param>
        /// <returns>返回统一的格式给API消费者</returns>
        protected ApiResult<T> GenActionResultGenericEx<T>(T? datas, string message = "", ApiResultCode code = ApiResultCode.Success, Exception ex = null) {
            //异常不为空
            if(ex != null) {
                code = ApiResultCode.Failed;
                message = string.IsNullOrWhiteSpace(message) ? ex.Message : $"{message}\n{ex.Message}";
#if Debug
                message = string.IsNullOrWhiteSpace(message) ? ex.Message : $"{message}\n{ex.Message}";
#endif
            } else if(datas == null) {
                code = ApiResultCode.Empty;
                message = "数据为空";
            }

            var result = new ApiResult<T> {
                Code = code,
                Message = message,
                Data = datas
            };
            return result;
        }
    }
}
