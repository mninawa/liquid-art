using Template.Shared;

namespace Template.Business.Abstraction
{
    public interface IBusiness
    {
        Task CreateTemplate(TemplateInsertDto insertDto, CancellationToken cancellationToken = default);
        Task<TemplateInsertDto?> GetTemplateById(string id, CancellationToken cancellationToken = default);
        Task<List<TemplateInsertDto>> GetAllTemplates(int numberOfRecord,CancellationToken cancellationToken = default);
        Task<bool> UpdateTemplate(string id, TemplateUpdateDto updateDto, CancellationToken cancellationToken = default);
        Task<bool> Delete(string id, CancellationToken cancellationToken = default);
      
    }
}
