using Aurora.Backend.Users.Services.Enumerables;

namespace Aurora.Backend.Users.Services.Models
{
    [Serializable]
    public class Result<T>
    {
        public EResult Status { get; set; }
        public string Message { get; set; }
        public T Response { get; set; }

        public Result()
        {
        }

        public Result(EResult status)
        {
            Status = status;
        }

        public Result(EResult status, T response)
        {
            Status = status;
            Response = response;
        }

        public Result(Exception ex)
        {
            Status = EResult.ERROR;
            Message = ex.Message;
        }
    }
}

