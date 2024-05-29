using Mango.web.Models;

namespace Mango.web.Service.Iservice
{
    public interface IBaseService
    {
      Task< ResponseDto?>  sendAsync(RequestDto requestDto);
    }
}
