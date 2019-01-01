

namespace ColoPay.Model.SysManage
{
    public enum ApplicationKeyType
    {
        None = -1,
        System = 1,
        SNS = 2,
        Shop = 3,
        CMS = 4, //DONE: BEN MODIFY 根据 SA_Config_Type 表规范枚举值 20121119
        OpenAPI = 5,
        Mobile = 6
    }
}
