using System.Data.SqlTypes;

namespace CoffeeManagement.MetaData
{
    public class ApiResponseBuilder
    {
        // this method is used to build  a response object for single data 
        public static ApiResponse<T> BuildResponse<T>(int statusCode, string message, T data, string reason = null)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                Data = data,
                IsSuccess = statusCode >= 200 && statusCode < 300,
                Reason = reason
            };
        }


        // this method is used to build a response object for erro response
        public static ApiResponse<T> BuildErrorsResponse<T>(T data, int statusCode, string message, string reason)
        {
            return new ApiResponse<T>
            {
                Data = data,
                StatusCode = statusCode,
                Message = message,
                Reason = reason,
                IsSuccess = false
            };
        }
        // this method is used to build a response object for list/pagination data
        public static ApiResponse<PagingResponse<T>> BuiderpageResponse<T>(
            IEnumerable<T> items,
            int totalPages,
            int currentPage,
            int pageSize,
            long totalItems,
            string message)
        {
            var pagedResponse = new PagingResponse<T>
            {
                Items = items,
                Meta = new PaginationMeta
                {
                    TotalItems = totalItems,
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                }
            };

            return new ApiResponse<PagingResponse<T>>
            {
                Data = pagedResponse,
                Message = message,
                StatusCode = 200,
                IsSuccess = true,
                Reason = null
            };
        }
    }
}
