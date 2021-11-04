using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Input;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClickHouseController : ControllerBase
    {
        private readonly IFreeSql _fsql;

        public ClickHouseController(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        //----------------------Log引擎 不支持修改 删除操作-----//

        //单表查询
        public async Task<List<Student>> GetStudents()
        {
            //var students = (await _fsql.Ado.QueryAsync<Student>("select a.Id,a.Name from student a")).ToList();

            var students =  await _fsql.Select<Student>().ToListAsync();

            return students;
        }

        //新增
        public async Task AddStudent([FromBody] AddStudentInput input)
        {
            var student = new Student { Id = Guid.NewGuid(), Name = input.Name };

            await _fsql.Insert(student).ExecuteAffrowsAsync();
        }

        //批量新增
        public async Task BulkAddStudent([FromBody] List<AddStudentInput> inputs)
        {
            var students = inputs.Select(a => new Student { Id = Guid.NewGuid(), Name = a.Name });
            await _fsql.Insert(students).ExecuteAffrowsAsync();
        }


        //----------------------MergeTree引擎 支持修改 删除操作-----//

        //新增
        public async Task AddTeacher([FromBody] AddTeacherInput input)
        {
            var item = new Teacher { Id = Guid.NewGuid(), Name = input.Name, Birthday=DateTime.Now  };

            await _fsql.Insert(item).ExecuteAffrowsAsync();
        }


        //更新
        [HttpPut("{id}")]
        public async Task UpdateTeacher(Guid id)
        {
            var item = await _fsql.Select<Teacher>().Where(a => a.Id == id).ToOneAsync();

            if (item == null) return;

            item.Name = item.Name.Split('-')[0] + "-" + new Random().Next(1, 100);

            string sql = _fsql.Update<Teacher>().SetSource(item).ToClickHouseUpdateSql();

            await _fsql.Ado.ExecuteNonQueryAsync(sql);
        }

        //删除
        [HttpDelete("{id}")]
        public async Task DeleteTeacher(Guid id)
        {
            var item = await _fsql.Select<Teacher>().Where(a => a.Id == id).ToOneAsync();

            if (item == null) return;

            string sql = _fsql.Delete<Teacher>(item).ToClickHouseDeleteSql();

            await _fsql.Ado.ExecuteNonQueryAsync(sql);
        }
    }
}
  