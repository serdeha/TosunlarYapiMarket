using System.Linq.Expressions;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Data.UnitOfWork;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Concrete
{
    public class NoteManager:INoteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NoteManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Note? note)
        {
            if (note != null)
            {
                _unitOfWork.Notes.Add(note);
                _unitOfWork.SaveChanges();
            }
        }

        public void Update(Note? note)
        {
            if (note != null)
            {
                _unitOfWork.Notes.Update(note);
                _unitOfWork.SaveChanges();
            }
        }

        public void Delete(Note? note)
        {
            if (note != null)
            {
                note.IsActive = false;
                note.IsDeleted = true;
                _unitOfWork.Notes.Delete(note);
                _unitOfWork.SaveChanges();
            }
        }

        public void HardDelete(Note? note)
        {
            if (note != null)
            {
                _unitOfWork.Notes.HardDelete(note);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task<int> AddAsync(Note? note)
        {
            if (note != null)
            {
                await _unitOfWork.Notes.AddAsync(note);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> UpdateAsync(Note? note)
        {
            if (note != null)
            {
                await _unitOfWork.Notes.UpdateAsync(note);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> DeleteAsync(Note? note)
        {
            if (note != null)
            {
                note.IsActive = false;
                note.IsDeleted = true;
                await _unitOfWork.Notes.DeleteAsync(note);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public async Task<int> HardDeleteAsync(Note? note)
        {
            if (note != null)
            {
                await _unitOfWork.Notes.HardDeleteAsync(note);
                return await _unitOfWork.SaveChangesAsync();
            }

            return 0;
        }

        public List<Note>? GetAll(Expression<Func<Note, bool>>? filter = null)
        {
            return filter == null ? _unitOfWork.Notes.GetAll() : _unitOfWork.Notes.GetAll(filter);
        }

        public List<Note>? GetAllWithFilter(Expression<Func<Note, bool>>? predicate = null, params Expression<Func<Note, object>>[] includeProperties)
        {
            return _unitOfWork.Notes.GetAllWithFilter(predicate, includeProperties);
        }

        public async Task<List<Note>>? GetAllWithFilterAsync(Expression<Func<Note, bool>>? predicate = null, params Expression<Func<Note, object>>[] includeProperties)
        {
            return await _unitOfWork.Notes.GetAllWithFilterAsync(predicate, includeProperties)!;
        }

        public Note? GetWithFilter(Expression<Func<Note, bool>>? predicate = null, params Expression<Func<Note, object>>[] includeProperties)
        {
            return _unitOfWork.Notes.GetWithFilter(predicate, includeProperties);
        }

        public async Task<Note?> GetWithFilterAsync(Expression<Func<Note, bool>>? predicate = null, params Expression<Func<Note, object>>[] includeProperties)
        {
            return await _unitOfWork.Notes.GetWithFilterAsync(predicate, includeProperties);
        }

        public Note? GetById(int noteId)
        {
            return _unitOfWork.Notes.GetById(x => x.Id == noteId);
        }

        public async Task<Note?> GetByIdAsync(int noteId)
        {
            return await _unitOfWork.Notes.GetByIdAsync(x => x.Id == noteId);
        }

        public int GetCount(Expression<Func<Note, bool>>? filter = null)
        {
            return _unitOfWork.Notes.GetCount(filter);
        }

        public async Task<int> GetCountAsync(Expression<Func<Note, bool>>? filter = null)
        {
            return await _unitOfWork.Notes.GetCountAsync(filter);
        }
    }
}
