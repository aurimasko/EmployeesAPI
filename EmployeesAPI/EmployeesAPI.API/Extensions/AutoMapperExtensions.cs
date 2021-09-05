using AutoMapper;
using EmployeesAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.API.Extensions
{
    public static class AutoMapperExtensions
    {

        public static Response<T> ToDTO<T, K>(this IMapper mapper, Response<K> response) 
        { 
            if (response.IsSuccess)
            {
                var mapped = mapper.Map<K, T>(response.Content);
                return new Response<T>(mapped);
            }
            else
            {
                //This is used when entity is returned together with an error
                //Example: concurrency error
                T content = default;

                if (response.Content != null)
                    content = mapper.Map<K, T>(response.Content);

                return new Response<T>(response.ErrorMessages)
                {
                    Content = content,
                    ErrorCodes = response.ErrorCodes,
                    InnerException = response.InnerException
                };
            }
        }

        public static Response<T> ToPOCO<T, K>(this IMapper mapper, Response<K> response) 
        {
            if (response.IsSuccess)
            {
                var mapped = mapper.Map<K, T>(response.Content);
                return new Response<T>(mapped);
            }
            else
            {
                //This is used when entity is returned together with an error
                //Example: concurrency error
                T content = default;

                if (response.Content != null)
                    content = mapper.Map<K, T>(response.Content);

                return new Response<T>(response.ErrorMessages)
                {
                    Content = content,
                    ErrorCodes = response.ErrorCodes,
                    InnerException = response.InnerException
                };
            }
        }

        public static Response<IEnumerable<T>> ToDTO<T, K>(this IMapper mapper, Response<IEnumerable<K>> response) 
        {
            if (response.IsSuccess)
            {
                var mapped = mapper.Map<IEnumerable<K>, List<T>>(response.Content);
                return new Response<IEnumerable<T>>(mapped);
            }
            else
            {
                //This is used when entity is returned together with an error
                //Example: concurrency error
                List<T> content = null;

                if (response.Content != null)
                    content = mapper.Map<IEnumerable<K>, List<T>>(response.Content);

                return new Response<IEnumerable<T>>(response.ErrorMessages)
                {
                    Content = content,
                    ErrorCodes = response.ErrorCodes,
                    InnerException = response.InnerException
                };
            }
        }

        public static Response<IEnumerable<T>> ToPOCO<T, K>(this IMapper mapper, Response<IEnumerable<K>> response) 
        {
            if (response.IsSuccess)
            {
                var mapped = mapper.Map<IEnumerable<K>, IEnumerable<T>>(response.Content);
                return new Response<IEnumerable<T>>(mapped);
            }
            else
            {
                //This is used when entity is returned together with an error
                //Example: concurrency error
                List<T> content = null;

                if (response.Content != null)
                    content = mapper.Map<IEnumerable<K>, List<T>>(response.Content);

                return new Response<IEnumerable<T>>(response.ErrorMessages)
                {
                    Content = content,
                    ErrorCodes = response.ErrorCodes,
                    InnerException = response.InnerException
                };
            }
        }
    }
}
