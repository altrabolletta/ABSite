using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Facebook;
using Owin.Security.Providers.LinkedIn;
using System.Security.Claims;
using System.Threading.Tasks;
using Owin;
using ABSite.Models;

namespace ABSite
{
    public partial class Startup
    {
        // Per altre informazioni su come configurare l'autenticazione, vedere https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configurare il contesto di database, la gestione utenti e la gestione accessi in modo da usare un'unica istanza per richiesta
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Consentire all'applicazione di utilizzare un cookie per memorizzare informazioni relative all'utente connesso
            // e per memorizzare temporaneamente le informazioni relative a un utente che accede tramite un provider di accesso di terze parti
            // Configurare il cookie di accesso
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Consente all'applicazione di convalidare l'indicatore di sicurezza quando l'utente esegue l'accesso.
                    // Questa funzionalità di sicurezza è utile quando si cambia una password o si aggiungono i dati di un account di accesso esterno all'account personale.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser, long>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager),
                    getUserIdCallback: (id) => (id.GetUserId<long>()))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Consente all'applicazione di memorizzare temporaneamente le informazioni dell'utente durante la verifica del secondo fattore nel processo di autenticazione a due fattori.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Consente all'applicazione di memorizzare il secondo fattore di verifica dell'accesso, ad esempio il numero di telefono o l'indirizzo e-mail.
            // Una volta selezionata questa opzione, il secondo passaggio di verifica durante la procedura di accesso viene memorizzato sul dispositivo usato per accedere.
            // È simile all'opzione RememberMe disponibile durante l'accesso.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Rimuovere il commento dalle seguenti righe per abilitare l'accesso con provider di accesso di terze parti
            app.UseMicrosoftAccountAuthentication(
               clientId: "xxxxxxxxxxxxxxxxxx",
               clientSecret: "xxxxxxxxxxxxxxxxxx");


            app.UseTwitterAuthentication(
                consumerKey: "xxxxxxxxxxxxxxxxxx",
                consumerSecret: "xxxxxxxxxxxxxxxxxx");

            var facebookOptions = new FacebookAuthenticationOptions
            {
                AppId = "xxxxxxxxxxxxxxxxxx",
                AppSecret = "xxxxxxxxxxxxxxxxxx",
                //BackchannelHttpHandler = new FacebookBackChannelHandler(),
                UserInformationEndpoint = "https://graph.facebook.com/v2.11/me?fields=id,email,picture",
                Provider = new FacebookAuthenticationProvider()
                {
                    OnAuthenticated = (context) =>
                    {
                        context.Identity.AddClaim(new Claim("urn:facebook:access_token", context.AccessToken, ClaimValueTypes.String, "Facebook"));
                        context.Identity.AddClaim(new Claim("urn:facebook:email", context.Email, ClaimValueTypes.Email, "Facebook"));
                        return Task.FromResult(0);
                    }
                },
            };
            facebookOptions.Scope.Add("email");
            facebookOptions.Scope.Add("user_friends");
            //facebookOptions.Scope.Add("friends_about_me");
            //facebookOptions.Scope.Add("friends_photos");
            app.UseFacebookAuthentication(facebookOptions);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "xxxxxxxxxxxxxxxxxx",
                ClientSecret = "xxxxxxxxxxxxxxxxxx"

            });

            var linkedInOptions = new LinkedInAuthenticationOptions
            {
                ClientId = "xxxxxxxxxxxxxxxxxx",
                ClientSecret = "xxxxxxxxxxxxxxxxxx",
                Provider = new LinkedInAuthenticationProvider()
                {
                    OnAuthenticated = (context) =>
                    {
                        context.Identity.AddClaim(new Claim("urn:linkedin:accesstoken", context.AccessToken, ClaimValueTypes.String, "LinkedIn"));
                        context.Identity.AddClaim(new Claim("urn:linkedin:name", context.Name));
                        context.Identity.AddClaim(new Claim("urn:linkedin:email", context.Email, ClaimValueTypes.Email, "LinkedIn"));
                        context.Identity.AddClaim(new Claim("urn:linkedin:id", context.Id));
                        return Task.FromResult(0);
                    }
                },
            };
            app.UseLinkedInAuthentication(linkedInOptions);


            //app.UseYahooAuthentication("", "");

            //app.UseInstagramInAuthentication("", "");

            //var options = new GooglePlusAuthenticationOptions
            //{
            //    ClientId = "",
            //    ClientSecret = "",
            //    RequestOfflineAccess = true,
            //    Provider = new GooglePlusAuthenticationProvider
            //    {
            //        OnAuthenticated = async context => System.Diagnostics.Debug.WriteLine(String.Format("Refresh Token: {0}", context.RefreshToken))
            //    }
            //};
            //options.MomentTypes.Add("http://schemas.google.com/AddActivity");
            //options.MomentTypes.Add("http://schemas.google.com/CheckInActivity");
            //options.MomentTypes.Add("http://schemas.google.com/BuyActivity");
            //app.UseGooglePlusAuthentication(options);

            //app.UseOpenIDAuthentication("http://me.yahoo.com/", "Yahoo");

            //app.UseOpenIDAuthentication("https://www.google.com/accounts/o8/id", "Google");

            //app.UseDropboxAuthentication(
            //    appKey: "",
            //    appSecret: "");

            //app.UseFoursquareAuthentication(
            //	clientId: "",
            //	clientSecret: "");

            //app.UseFlickrAuthentication("", "");

            //app.UseSpotifyAuthentication(
            //    clientId: "",
            //    clientSecret: "");

        }
    }
}