using System;
using System.Collections.Generic;
using System.Text;

namespace MinorFeature.BLL
{
    /// <summary>
    /// 处理结果封装类
    /// </summary>
    public class ProcessResult
    {
        /// <summary>
        /// 结果代码
        /// </summary>
        public StatusCode RCode { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 隐式类型转换bool的泛型结果
        /// </summary>
        /// <param name="result">需要转换的对象</param>
        public static implicit operator ProcessResult<bool>(ProcessResult result)
        {
            return new ProcessResult<bool>()
            {
                RCode = result.RCode,
                Message = result.Message
            };
        }
    }
    /// <summary>
    /// 处理结果封装类
    /// </summary>
    public class ProcessResult<T>
    {
        /// <summary>
        /// 结果代码
        /// </summary>
        public StatusCode RCode { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 结果数据
        /// </summary>
        public T ResultData { get; set; }
    }
    /// <summary>
    /// 结果代码
    /// </summary>
    public enum StatusCode
    {
        /// <summary>
        /// 拒绝访问
        /// </summary>
        Refuse = -1,
        /// <summary>
        /// 处理成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 数据已经存在
        /// </summary>
        Exists,
        /// <summary>
        /// 参数不能为空
        /// </summary>
        NotEmpty,
        /// <summary>
        /// 参数错误
        /// </summary>
        ParamError,
        /// <summary>
        /// 没有对应数据
        /// </summary>
        NotFound
    }
}
