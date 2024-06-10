using Mango.web.Models;
using Mango.web.Service.Iservice;
using Newtonsoft.Json;
using System.Text;

namespace Mango.web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;

        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {

            try
            {


                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");


                if (withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(requestDto.Url);

                if (requestDto.Data != null)
                {

                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");

                }


                HttpResponseMessage? apiResponse = null;

                switch (requestDto.ApiType)
                {
                    case Utility.SD.ApiType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case Utility.SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case Utility.SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case Utility.SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new ResponseDto() { isSuccess = false, Message = "Not Found" };

                    case System.Net.HttpStatusCode.Forbidden:
                        return new ResponseDto()
                        {
                            isSuccess = false,
                            Message = "Access Denied"
                        };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new ResponseDto() { isSuccess = false, Message = "Unauthorized" };

                    case System.Net.HttpStatusCode.InternalServerError:
                        return new ResponseDto() { isSuccess = false, Message = "Internal Server Error" };

                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;

                }


            }
            catch (Exception ex)
            {

                var dto = new ResponseDto { isSuccess = false, Message = ex.Message };
                return dto;
            }


        }
    }
}
