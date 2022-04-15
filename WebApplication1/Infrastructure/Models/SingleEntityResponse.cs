namespace WebApplication1.Infrastructure.Models
{
    public class SingleEntityResponse<T>
    {
        public SingleEntityResponse(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
    }
}
