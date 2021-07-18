namespace DevBase.Services.DTO
{
    public class ResponseDto<T>
    {
        public ResponseDto()
        {
        }
        public ResponseDto(T data)
        {
            Sucesso = true;
            Mensagem = string.Empty;
            Erros = null;
            Data = data;
        }
        public T Data { get; set; }
        public bool Sucesso { get; set; }
        public string[] Erros { get; set; }
        public string Mensagem { get; set; }
    }
}
