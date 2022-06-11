using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
using ArtPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtPortfolio.Services
{
    public class ProjectBiographyService : IModelOptionSelector<ProjectBiography>
    {
        private ArtPortfolioDbContext _ctx;
        ILogger<ProjectBiographyService> _logger;

        public ProjectBiographyService(ArtPortfolioDbContext ctx, ILogger<ProjectBiographyService> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }
        public async Task SelectAsync(Guid id)
        {
            try
            {
               await SelectAndUpdate(id);
            }
            catch (Exception e)
            {
                _logger.LogError(String.Format("Could not select and update bio with id {0} in {1}: {2}",
                    id,nameof(SelectAsync), e
                ));
            }
        }

        private async Task SelectAndUpdate(Guid id)
        {
            var selected = await _ctx.ProjectBiographies.FirstOrDefaultAsync(b => b.Id == id);
            if (selected == null)
            {
                throw new ArgumentException(String.Format("Could not find bio with provided id: {0} to select bio in {1}.", id, nameof(SelectAsync)));
            }
            // If the bio is already selected don't waste resources, exit early.
            if (selected.Selected)
            {
                return;
            }
            var oldSelected = await _ctx.ProjectBiographies.FirstOrDefaultAsync(b => b.Selected == true);
            if (oldSelected != null)
            {
                oldSelected.Selected = false;
                _ctx.ProjectBiographies.Update(oldSelected);

            }
            // Updated the selected bio finally.
            selected.Selected = true;
            _ctx.ProjectBiographies.Update(selected);
            await _ctx.SaveChangesAsync();
        }
    }
}
