using Kashly.Category.Communication.Requests;

namespace Kashly.Category.Application.UseCases.Archive;

public interface IArchiveCategoryUseCase
{
    Task Handle(ArchiveCategoryRequest request, CancellationToken cancellationToken);
}
