namespace SpacecraftControlSystem.Models
{
    public class ResponseBase<T> : ResponseBaseEmpty
    {
        public ResponseBase(bool success)
        {
            Meta = new ResponseBaseEmptyMetaModel
            {
                Success = success
            };
        }

        public ResponseBase(bool success, string message)
        {
            Meta = new ResponseBaseEmptyMetaModel
            {
                Success = success,
                Message = message
            };
        }

        public ResponseBase(bool success, T data, string? message = null) : base(success, message)
        {
            Data = data;
        }

        public T? Data { get; set; }
    }
}
