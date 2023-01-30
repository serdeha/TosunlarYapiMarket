using System.Linq.Expressions;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Abstract
{
    public interface INoteService
    {
        void Add(Note note);
        void Update(Note note);
        void Delete(Note note);
        void HardDelete(Note note);
        Task<int> AddAsync(Note note);
        Task<int> UpdateAsync(Note note);
        Task<int> DeleteAsync(Note note);
        Task<int> HardDeleteAsync(Note note);
        List<Note>? GetAll(Expression<Func<Note, bool>>? filter = null);
        List<Note>? GetAllWithFilter(Expression<Func<Note, bool>>? predicate = null, params Expression<Func<Note, object>>[] includeProperties);
        Task<List<Note>>? GetAllWithFilterAsync(Expression<Func<Note, bool>>? predicate = null, params Expression<Func<Note, object>>[] includeProperties);
        Note? GetWithFilter(Expression<Func<Note, bool>>? predicate = null, params Expression<Func<Note, object>>[] includeProperties);
        Task<Note?> GetWithFilterAsync(Expression<Func<Note, bool>>? predicate = null, params Expression<Func<Note, object>>[] includeProperties);
        Note? GetById(int noteId);
        Task<Note?> GetByIdAsync(int noteId);
        int GetCount(Expression<Func<Note, bool>>? filter = null);
        Task<int> GetCountAsync(Expression<Func<Note, bool>>? filter = null);
    }
}
