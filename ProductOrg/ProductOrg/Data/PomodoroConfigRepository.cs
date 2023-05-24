using ProductOrg.Models;
using System.Linq;

namespace ProductOrg.Repositories
{
    public class PomodoroConfigRepository
    {
        #region Public Methods

        public PomodoroConfiguration LoadAsync()
        {
            PomodoroConfiguration config = null;

            using (var context = new ProductOrgContext())
            {
                config = context.PomodoroConfigurations.FirstOrDefault();
                if (config == null)
                {
                    config = LoadConfigDefault();
                }
            }

            return config;
        }

        public async void SaveAsync(PomodoroConfiguration config)
        {
            DeleteAsync();
            using (var context = new ProductOrgContext())
            {
                context.PomodoroConfigurations.Add(config);
                await context.SaveChangesAsync();
            }
        }

        #endregion
        #region Private Methods

        private async void DeleteAsync()
        {
            using (var context = new ProductOrgContext())
            {
                var configs = context.PomodoroConfigurations.ToList();
                context.PomodoroConfigurations.RemoveRange(configs);
                await context.SaveChangesAsync();
            }
        }

        private PomodoroConfiguration LoadConfigDefault()
        {
            var config = new PomodoroConfiguration()
            {
                LongBreak = 20,
                ShortBreak = 5,
                Pomorodos = 4,
                Working = 25
            };

            SaveAsync(config);
            return config;
        }

        #endregion
    }
}
