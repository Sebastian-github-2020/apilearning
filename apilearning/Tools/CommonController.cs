using Microsoft.AspNetCore.Mvc;

namespace apilearning.Tools {
    /// <summary>
    /// 封装一个通用的ControllerBase
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CommonController : ControllerBase {
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
