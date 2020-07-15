using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalShopApp
{
    public enum BtnMode
    {
        NONE, // 기본상태
        INSERT,
        UPDATE,
        DELETE,
        SELECT
    }

    public class Commons
    {
        // 공통 DB 연결 문자열
        public static readonly string CONNSTR =
            "Data Source=localhost;Port=3306;Database=bookrentalshop;Uid=root;Password=mysql_p@ssw0rd;";
    }
}
