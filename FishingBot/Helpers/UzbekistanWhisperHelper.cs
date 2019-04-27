using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FishingBot.Helpers
{
  public class UzbekistanWhisperHelper
  {
    private List<UzbekistanianPhrase> phrases = new List<UzbekistanianPhrase>( );
    public void WhisperPhrase()
    {
      phrases.Add(new UzbekistanianPhrase { Message = "Xush kelibsiz" });
      phrases.Add(new UzbekistanianPhrase { Message = "Ishlaringiz yaxshimi?" });
      phrases.Add(new UzbekistanianPhrase { Message = "Anchadan beri ko'rishmadik!" });
      phrases.Add(new UzbekistanianPhrase { Message = "Tanishganimdan hursandman" });
      phrases.Add(new UzbekistanianPhrase { Message = "Omad yor bo'lsin!" });
      phrases.Add(new UzbekistanianPhrase { Message = "Yahshi ishlang!" });
      phrases.Add(new UzbekistanianPhrase { Message = "Ha" });
      phrases.Add(new UzbekistanianPhrase { Message = "Men tushunmayapman" });
      phrases.Add(new UzbekistanianPhrase { Message = "O'zbek tilida gapirasizmi?" });
      phrases.Add(new UzbekistanianPhrase { Message = "Afu eting" });
      phrases.Add(new UzbekistanianPhrase { Message = "Bu odam hammasi uchun to'laydi" });
      phrases.Add(new UzbekistanianPhrase { Message = "Bir til aslo yetarli emas" });
      phrases.Add(new UzbekistanianPhrase { Message = "Mening havo yostiqli kemam ilonbalig'i bilan to'lgan" });
      var random = new Random();
      var chosenPhrase = phrases[random.Next(0, phrases.Count)].Message;
      foreach (char c in chosenPhrase)
        SendKeys.SendWait(c.ToString());
    }
     protected class UzbekistanianPhrase
    {
      public string Message { get; set; }
    }
  }
 
}
