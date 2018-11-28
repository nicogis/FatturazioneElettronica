namespace FatturazioneElettronica
{
    using System;
    using System.Linq;

    internal static class UriExtension
    {
        /// <summary>
        /// Combines the Uri with a base path and the relative url into one, consolidating the '/' between them
        /// </summary>
        /// <param name="urlBase">Base Uri that will be combined</param>
        /// <param name="relativeUrl">The relative path to combine</param>
        /// <returns>The merged Uri</returns>
        public static Uri Combine(this Uri baseUri, string relativeUrl)
        {
            if (baseUri == null)
                throw new ArgumentNullException(nameof(baseUri));

            return new Uri(UrlCombine.Combine(baseUri.AbsoluteUri, relativeUrl));
        }

        /// <summary>
        /// Combines the Uri with base path and the array of relative urls into one, consolidating the '/' between them
        /// </summary>
        /// <param name="urlBase">Base Uri that will be combined</param>
        /// <param name="relativeUrl">The array of relative paths to combine</param>
        /// <returns>The merged Uri</returns>
        public static Uri Combine(this Uri baseUri, params string[] relativePaths)
        {
            if (baseUri == null)
                throw new ArgumentNullException(nameof(baseUri));

            return new Uri(UrlCombine.Combine(baseUri.AbsoluteUri, relativePaths));
        }
    }
    internal static class UrlCombine
    {
        /// <summary>
        /// Combines the url base and the relative url into one, consolidating the '/' between them
        /// </summary>
        /// <param name="urlBase">Base url that will be combined</param>
        /// <param name="relativeUrl">The relative path to combine</param>
        /// <returns>The merged url</returns>
        public static string Combine(string baseUrl, string relativeUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl));

            if (string.IsNullOrWhiteSpace(relativeUrl))
                return baseUrl;

            baseUrl = baseUrl.TrimEnd('/');
            relativeUrl = relativeUrl.TrimStart('/');

            return $"{baseUrl}/{relativeUrl}";
        }

        /// <summary>
        /// Combines the url base and the array of relatives urls into one, consolidating the '/' between them
        /// </summary>
        /// <param name="urlBase">Base url that will be combined</param>
        /// <param name="relativeUrl">The array of relative paths to combine</param>
        /// <returns>The merged url</returns>
        public static string Combine(string baseUrl, params string[] relativePaths)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl));

            if (relativePaths.Length == 0)
                return baseUrl;

            var currentUrl = Combine(baseUrl, relativePaths[0]);

            return Combine(currentUrl, relativePaths.Skip(1).ToArray());
        }
    }
    internal static class StringExtension
    {
        /// <summary>
        /// Combines the url base and the relative url into one, consolidating the '/' between them
        /// </summary>
        /// <param name="urlBase">Base url that will be combined</param>
        /// <param name="relativeUrl">The relative path to combine</param>
        /// <returns>The merged url</returns>
        public static string CombineUrl(
            this string urlBase,
            string relativeUrl) =>
            UrlCombine.Combine(urlBase, relativeUrl);

        /// <summary>
        /// Combines the url base and the array of relative urls into one, consolidating the '/' between them
        /// </summary>
        /// <param name="urlBase">Base url that will be combined</param>
        /// <param name="relativeUrl">The array of relative paths to combine</param>
        /// <returns>The merged url</returns>
        public static string CombineUrl(
            this string urlBase,
            params string[] relativeUrls) =>
            UrlCombine.Combine(urlBase, relativeUrls);
    }


}
