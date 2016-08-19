using System.Collections.Generic;

namespace KarmicEnergy.Core.Services.Results
{
    public class ServiceResult : IServiceResult
    {
        private readonly static ServiceResult _success;

        /// <summary>
        ///     List of errors
        /// </summary>
        public IEnumerable<string> Errors
        {
            get;
            private set;
        }

        /// <summary>
        ///     True if the operation was successful
        /// </summary>
        public bool Succeeded
        {
            get;
            private set;
        }

        /// <summary>
        ///     Static success result
        /// </summary>
        /// <returns></returns>
        public static ServiceResult Success
        {
            get
            {
                return ServiceResult._success;
            }
        }

        static ServiceResult()
        {
            ServiceResult._success = new ServiceResult(true);
        }

        /// <summary>
        ///     Failure constructor that takes error messages
        /// </summary>
        /// <param name="errors"></param>
        public ServiceResult(params string[] errors)
            : this((IEnumerable<string>)errors)
        {
        }

        /// <summary>
        ///     Failure constructor that takes error messages
        /// </summary>
        /// <param name="errors"></param>
        public ServiceResult(IEnumerable<string> errors)
        {
            if (errors == null)
            {
                errors = new string[] { KarmicEnergy.Core.Resources.ResultResource.DefaultError };
            }

            this.Succeeded = false;
            this.Errors = errors;
        }

        /// <summary>
        /// Constructor that takes whether the result is successful
        /// </summary>
        /// <param name="success"></param>
        protected ServiceResult(bool success)
        {
            this.Succeeded = success;
            this.Errors = new string[0];
        }

        /// <summary>
        ///     Failed helper method
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static ServiceResult Failed(params string[] errors)
        {
            return new ServiceResult(errors);
        }
    }
}
