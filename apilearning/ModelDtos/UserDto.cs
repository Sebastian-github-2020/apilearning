namespace apilearning.ModelDtos {
    /// <summary>
    /// 返回给用户的数据模型，对数据进行处理后返回
    /// 面向外部的Model
    /// 为了数据的健壮，可靠更易于进化
    /// 1. 如果没有
    /// </summary>
    public class UserDto {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public double Salary { get; set; }

        public string BornDate { get; set; } = null!;


    }
}
