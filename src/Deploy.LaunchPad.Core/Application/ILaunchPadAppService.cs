// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ILaunchPadAppService.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Application.Dto
{
    /// <summary>
    /// This interface marks all LaunchPad App Services and ensures a consistent set of methods for manipulating the domain entities.
    /// </summary>
    public partial interface ILaunchPadAppService
    {

        /// <summary>
        /// Creates the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Task&lt;ICanBeAppServiceMethodOutput&gt;.</returns>
        Task<ICanBeAppServiceMethodOutput> Create(ICanBeAppServiceMethodInput input);

        /// <summary>
        /// Gets the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Task&lt;ICanBeAppServiceMethodOutput&gt;.</returns>
        Task<ICanBeAppServiceMethodOutput> Get(ICanBeAppServiceMethodInput input);

        /// <summary>
        /// Gets the detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Task&lt;ICanBeAppServiceMethodOutput&gt;.</returns>
        Task<ICanBeAppServiceMethodOutput> GetDetail(ICanBeAppServiceMethodInput input);

        /// <summary>
        /// Gets the full.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Task&lt;ICanBeAppServiceMethodOutput&gt;.</returns>
        Task<ICanBeAppServiceMethodOutput> GetFull(ICanBeAppServiceMethodInput input);

        /// <summary>
        /// Gets the admin.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Task&lt;ICanBeAppServiceMethodOutput&gt;.</returns>
        Task<ICanBeAppServiceMethodOutput> GetAdmin(ICanBeAppServiceMethodInput input);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Task&lt;ICanBeAppServiceMethodOutput&gt;.</returns>
        Task<ICanBeAppServiceMethodOutput> GetAll(ICanBeAppServiceMethodInput input);

        /// <summary>
        /// Updates the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Task&lt;ICanBeAppServiceMethodOutput&gt;.</returns>
        Task<ICanBeAppServiceMethodOutput> Update(ICanBeAppServiceMethodInput input);

        /// <summary>
        /// Deletes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Task.</returns>
        Task Delete(ICanBeAppServiceMethodInput input);

    }
}
