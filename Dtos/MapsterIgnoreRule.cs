using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;

namespace backend.Dtos
{
    public class MapsterIgnoreRule
    {
        public static void register(){
            var config = TypeAdapterConfig.GlobalSettings;
            config.Default.IgnoreMember((member, context) =>
            {

                Type t = member.Type;
                if (t.IsPrimitive ||
                    t == typeof(string) ||
                    t == typeof(decimal) ||
                    t == typeof(DateTime) ||
                    t == typeof(Guid))
                    return false;
                if (t.IsGenericType &&
                    typeof(System.Collections.IEnumerable).IsAssignableFrom(t))
                    return true;
                if (t.IsClass)
                    return true;

                return false;
            });
        }

    }
}