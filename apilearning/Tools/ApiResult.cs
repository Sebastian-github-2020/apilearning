namespace apilearning.Tools {
    /// <summary>
    /// 统一返回结果泛型类ApiResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> {
        /// <summary>
        /// 错误代码
        /// </summary>
        public ApiResultCode Code { get; set; }
        /// <summary>
        /// 错误信息 
        /// </summary>
        public string Message { get; set; } = null!;
        /// <summary>
        /// 具体数据
        /// </summary>
        public T? Data { get; set; }
    }

    /// <summary>
    /// 状态码 枚举
    /// </summary>
    public enum ApiResultCode {
        //失败 
        Failed = 400,
        //成功
        Success = 200,
        //无权限
        NoAuth = 401,
        // 数据为空
        Empty = 201
    }
}
