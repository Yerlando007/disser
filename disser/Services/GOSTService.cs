﻿using disser.Interfaces;
using disser.Models.Base;
using disser.Models.EF.GOST;
using disser.Models.EF.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spire.Doc;
using Spire.Doc.Documents;
using System.Text;

namespace disser.Services
{
    public class GOSTService : IGOST
    {
        private readonly AppDbContext _db;
        public GOSTService(AppDbContext db)
            => _db = db;
        private static string _pathToImages = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//GOSTS");
        private static readonly List<string> _stopWords = new List<string>()
  {
   //ВВОДНЫЕ СЛОВА
   "а быть может", "а вернее", "а вернее сказать", "а впрочем", "а главное", "а значит", "а лучше сказать", "а между прочим", "а может быть",
   "а наоборот", "а например", "а следовательно", "а точнее", "без всякого сомнения", "без сомнения", "безусловно", "без шуток", "бесспорно",
   "благодарение богу", "бог даст", "более того", "больше того", "бывает", "бывало", "быть может", "вдобавок ко всему прочему", "верите ли",
   "веришь ли", "вернее", "вернее говоря", "вернее сказать", "верно", "вероятнее всего", "вероятно", "вестимо", "видать", "видимо", "видит бог",
   "видите ли", "видишь ли", "видно", "вишь", "в конце концов", "вне всякого сомнения", "вне сомнения", "в общем", "в общем-то", "во всяком случае",
   "во-вторых", "возможно", "воистину", "воля ваша", "воля твоя", "вообрази", "вообрази себе", "вообразите", "вообразите себе", "вообще", "вообще говоря",
   "вообще-то", "во-первых", "в принципе", "впрочем", "в самом деле", "в свою очередь", "в сущности", "в сущности говоря", "в-третьих", "в частности",
   "выходит", "главное", "главное дело", "главным делом", "говорят", "грешным делом", "грубо говоря", "да и то сказать", "дай бог память", "далее",
   "действительно", "делать нечего", "должно быть", "допустим", "другими словами", "если позволите", "если позволишь", "если угодно", "если хотите",
   "если хотите знать", "если хочешь", "если хочешь знать", "естественно", "еще лучше", "еще того лучше", "еще того хуже", "еще хуже", "жалко", "жаль",
   "знаете", "знаете ли", "знаешь", "знаешь ли", "знамо", "знамо дело", "знать", "значит", "значится", "и вообще", "известно", "и кроме того", "и лучше того",
   "и наоборот", "иначе говоря", "иначе сказать", "и однако", "и правда", "истинный бог", "и таким образом", "и того лучше", "и того хуже", "и хуже того",
   "кажется", "кажись", "казалось", "казалось бы", "как бы там ни было", "как бы то ни было", "как вам известно", "как видите", "как видишь", "как видно",
   "как водится", "как всегда", "как выяснилось", "как выясняется", "как говорилось", "как говорится", "как говорят", "как знать", "как известно", "как исключение",
   "как на заказ", "как назло", "как например", "как нарочно", "как ни говори", "как ни говорите", "как ни странно", "как обычно", "как оказалось", "как оказывается",
   "как перед Богом", "как по заказу", "как полагается", "как положено", "как правило", "как принято", "как принято говорить", "как сказано", "как сказать",
   "как следствие", "как хотите", "как хочешь", "как это ни странно", "к вашему сведению", "к несчастью", "ко всему прочему", "к огорчению", "конечно",
   "коротко говоря", "короче", "короче говоря", "к примеру", "к примеру сказать", "к прискорбию", "к радости", "к радости своей", "кроме всего прочего",
   "кроме того", "кроме этого", "кроме шуток", "к слову", "к слову сказать", "к сожалению", "кстати", "кстати говоря", "кстати сказать", "к стыду", "к стыду своему",
   "к счастью", "к твоему сведению", "к удивлению", "к ужасу", "к чести", "легко сказать", "лучше", "лучше сказать", "мало сказать", "мало того", "между нами",
   "между нами говоря", "между прочим", "может", "может быть", "может статься", "можно подумать", "можно сказать", "мол", "мягко выражаясь", "мягко говоря",
   "на беду", "на ваш взгляд", "наверно", "наверное", "надеюсь", "надо быть", "надо думать", "надо полагать", "называется", "наконец", "на мой взгляд",
   "на несчастье", "наоборот", "на первый взгляд", "например", "напротив", "на самом деле", "на счастье", "на твой взгляд", "натурально", "на худой конец",
   "небось", "несомненно", "нет слов", "нечего сказать", "ничего не скажешь", "но кроме того", "но вообще-то", "однако", "однако же", "одним словом", "оказывается",
   "определенно", "откровенно", "откровенно говоря", "откровенно сказать", "очевидно", "по-вашему", "по вашему мнению", "поверите ли", "поверишь ли", "по-видимому",
   "по видимости", "по всей вероятности", "по всей видимости", "по данным", "поди", "подумать только", "пожалуй", "пожалуйста", "по замыслу", "позволь", "позвольте",
   "по идее", "по-ихнему", "по крайней мере", "положим", "положимте", "получается", "помилуй", "помилуйте", "помимо всего", "помимо всего прочего", "помимо того",
   "помимо этого", "по мне", "по мнению", "помнится", "по-моему", "по моему мнению", "по-нашему", "понятно", "понятное дело", "по обыкновению", "по обычаю",
   "по определению", "по правде", "по правде говоря", "по правде сказать", "по преданию", "по прогнозам", "попросту говоря", "попросту сказать", "по сведениям",
   "по своему обыкновению", "по словам", "по слухам", "по совести", "по совести говоря", "по совести сказать", "по сообщению", "по сообщениям", "по справедливости",
   "по справедливости говоря", "по сути", "по сути дела", "по сути говоря", "по существу", "по существу говоря", "по счастью", "по-твоему", "по твоему мнению",
   "похоже", "по чести говоря", "по чести признаться", "по чести сказать", "почитай", "правда", "правду говоря", "правду сказать", "правильнее", "правильнее говоря",
   "правильнее сказать", "право", "право слово", "предположим", "предположительно", "представь", "представь себе", "представьте", "представьте себе", "прежде всего",
   "признаться", "признаться сказать", "признаюсь", "примерно", "против обыкновения", "проще говоря", "проще сказать", "разумеется", "само собой",
   "само собой разумеется", "с вашего позволения", "с вашего разрешения", "сверх того", "с другой стороны", "серьезно", "скажем", "сказать по правде",
   "сказать по совести", "сказать по чести", "скорее", "скорее всего", "слава богу", "следовательно", "слов нет", "словом", "случаем", "случайно", "слыхать",
   "слышно", "слышь", "слышь ты", "с моей точки зрения", "собственно", "собственно говоря", "с одной стороны", "соответственно", "со своей стороны",
   "с позволения сказать", "спрашивается", "стало быть", "с твоего позволения", "с твоего разрешения", "с точки зрения", "строго говоря", "судя по всему", "так",
   "таким образом", "так или иначе", "так сказать", "точнее", "точнее говоря", "точнее сказать", "факт", "хорошо", "чай", "часом", "чего доброго", "что и говорить",
   "что называется", "что ни говори", "что ни говорите", "шутка ли", "шутка ли сказать", "шутка сказать", "ясное дело",
   //ПРЕДЛОГИ
   "а-ля", "без", "без ведома", "безо", "безъ", "благодаря", "близ", "близко от", "в", "в виде", "в зависимости от", "в интересах", "в качестве", "в лице",
   "в отличие от", "в отношении", "в пандан", "в пользу", "в преддверии", "в продолжение", "в результате", "в роли", "в связи с", "в силу", "в случае",
   "в соответствии с", "в течение", "в целях", "в честь", "вблизи", "ввиду", "вглубь", "вдогон", "вдоль", "вдоль по", "взамен", "включая", "вкруг",
   "вместо", "вне", "внизу", "внутри", "внутрь", "во", "во имя", "во славу", "вовнутрь", "возле", "вокруг", "вопреки", "вослед", "впереди", "вплоть до",
   "впредь до", "вразрез", "вроде", "вслед", "вслед за", "вследствие", "встречу", "выключая", "для", "для-ради", "до", "за", "за вычетом", "за исключением",
   "за счёт", "за-ради", "замест", "заместо", "из", "из-за", "из-под", "из-подо", "изнутри", "изо", "исключая", "исходя из", "к", "касаемо", "касательно",
   "ко", "кончая", "кроме", "кругом", "лицом к лицу с", "меж", "между", "мимо", "на", "на благо", "на виду у", "на глазах у", "на предмет", "наверху",
   "навроде", "навстречу", "над", "надо", "назад", "назади", "назло", "накануне", "наместо", "наперекор", "наперерез", "наперехват", "наподобие",
   "наподобье", "напротив", "наряду с", "насупротив", "насчёт", "начиная с", "не без", "не считая", "невзирая на", "недалеко от", "независимо от", "несмотря",
   "несмотря на", "ниже", "о", "об", "обо", "обок", "обочь", "около", "окрест", "окроме", "окромя", "округ", "опосля", "опричь", "от", "от имени",
   "от лица", "относительно", "ото", "перед", "передо", "по", "по линии", "по мере", "по направлению к", "по отношению к", "по поводу", "по причине",
   "по случаю", "по сравнению с", "по-за", "по-над", "по-под", "поблизости от", "повдоль", "поверх", "под", "под видом", "под эгидой", "подле", "подо",
   "подобно", "позади", "позадь", "позднее", "помимо", "поперёд", "поперёк", "порядка", "посверху", "посереди", "посередине", "посерёдке", "посередь",
   "после", "посреди", "посредине", "посредством", "пред", "предо", "преж", "прежде", "при", "при помощи", "применительно к", "про", "промеж", "промежду",
   "против", "противно", "противу", "путём", "ради", "рядом с", "с", "с ведома", "с помощью", "с прицелом на",
   //СОЮЗЫ
   "а", "c", "м", "а вдобавок", "а именно", "а также", "а то", "благодаря тому, что", "благо", "буде", "будто", "вдобавок", "в результате чего", "в результате того, что",
   "в связи с тем, что", "в силу того, что", "в случае если", "в то время как", "в том случае если", "в силу чего", "ввиду того, что", "вопреки тому, что", "вроде того как",
   "вследствие чего", "вследствие того, что", "да вдобавок", "да еще", "да", "да и", "да и то", "дабы", "даже", "даром, что", "для того, чтобы", "же", "едва", "едва…, как",
   "едва…, лишь", "ежели", "если", "если бы", "если не…, то", "если…, то", "затем, чтобы", "затем, что", "зато", "зачем", "и", "и все же", "и значит", "а именно",
   "и поэтому", "и притом", "и все-таки", "и следовательно", "и то", "и тогда", "и еще", "и…, и", "ибо", "и вдобавок", "из-за того, что", "или", "или…, или", "кабы",
   "как", "Как скоро", "как будто", "как если бы", "как словно", "как только", "как…, так и", "как-то?", "когда", "когда…, то", "коли", "к тому же", "кроме того", "ли…, ли",
   "либо", "либо…, либо", "лишь", "лишь бы", "лишь только", "между тем как", "нежели", "не столько…, сколько", "не то…, не то", "не только не…, но и", "не только…, но и",
   "не только…., а и", "не только…, но даже", "невзирая на то, что", "независимо от того, что", "несмотря на то, что", "ни…, ни", "но", "однако", "особенно", "оттого",
   "оттого, что", "отчего", "перед тем как", "по мере того как", "по причине того, что", "подобно тому как", "пока", "покамест", "покуда", "пока не", "после того как",
   "поскольку", "потому", "потому, что", "почему", "прежде чем", "при всем том, что", "при условии, что", "притом", "причем", "пускай", "пусть", "ради того, чтобы",
   "раз", "раньше чем", "с тем, чтобы", "с тех пор как", "словно", "так же…, как", "так же…, как и", "так как", "так как…, то", "так, что", "также", "тем более, что",
   "тогда как", "то есть", "то ли…, то ли", "то…, то", "тоже", "только", "только бы", "только, что", "только лишь", "только чуть", "точно", "хотя", "хотя и…, но",
   "хотя…, но", "чем", "чем…, тем", "что", "чтоб", "чтобы", 
   //НАРЕЧИЯ
   "абсолютно", "автоматически", "адекватно", "азартно", "аккурат", "аккуратно", "активно", "бегло", "бегом", "бедно", "безвозвратно", "беззаботно", "беззвучно", "безмерно",
   "безнадежно", "безоговорочно", "безошибочно", "безразлично", "безумно", "безупречно", "безуспешно", "бережно", "бесконечно", "бесплатно", "бесповоротно", "беспокойно",
   "бесполезно", "беспомощно", "беспощадно", "беспрепятственно", "беспрерывно", "бессильно", "бесследно", "бессмысленно", "бессознательно", "бесшумно", "бешено",
   "биологически", "битком", "благополучно", "блаженно", "блестяще", "близко", "богато", "бодро", "более", "болезненно", "больно", "больше", "босиком", "буквально",
   "бурно", "быстренько", "быстро", "важно", "вблизи", "вбок", "вверх", "вверху", "ввысь", "вглубь", "вдалеке", "вдали", "вдаль", "вдвое", "вдвоем", "вдвойне", "вдобавок",
   "вдоволь", "вдогонку", "вдоль", "вдребезги", "вдруг", "вежливо", "везде", "великолепно", "вертикально", "весело", "весьма", "вечно", "взад", "взаимно", "взамен",
   "взволнованно", "видать", "видно", "виновато", "вира", "вконец", "вкратце", "вкупе", "вкусно", "властно", "влево", "влет", "вместе", "вмиг", "вначале", "внаем",
   "внакладе", "вне", "внезапно", "внешне", "вниз", "внизу", "внимательно", "вновь", "внутренне", "внутри", "внутрь", "внятно", "вовремя", "вовсе", "вовсю", "воедино",
   "возбужденно", "возле", "возможно", "возмущенно", "вокруг", "воочию", "вопросительно", "восторженно", "вот-вот", "впервые", "вперебежку", "вперед", "впереди", "вплотную",
   "вплоть", "вполголоса", "вполне", "впору", "впоследствии", "вправду", "вправе", "вправо", "впредь", "вприсядку", "впрок", "впрямь", "впустую", "враз", "вразвалку",
   "врасплох", "временно", "вручную", "вряд", "всего-навсего", "всего-то", "всемирно", "всерьез", "всецело", "вскользь", "вскоре", "вслед", "вслух", "вспять", "всячески",
   "втайне", "вторично", "втрое", "втроем", "вчера", "вчетвером", "выгодно", "вызывающе", "высоко", "выше", "вяло", "вдиковинку", "внасмешку", "врассрочку", "генетически",
   "гладко", "глубоко", "глупо", "глухо", "гневно", "гораздо", "гордо", "горестно", "горько", "горячо", "грамотно", "грозно", "громко", "грубо", "грустно", "грязно", "гулко",
   "густо", "давно", "давным-давно", "далее", "далеко", "даром", "дважды", "деликатно", "демонстративно", "детально", "дико", "динамично", "днем", "добровольно", "добродушно",
   "добросовестно", "доверительно", "доверху", "доверчиво", "довольно", "довольно-таки", "долго", "должно", "долой", "дома", "домой", "дополнительно", "дорого", "дословно",
   "досрочно", "достаточно", "достоверно", "достойно", "дружески", "дружно", "дурно", "духовно", "душевно", "душно", "дыбом", "едва", "единогласно", "единодушно", "ежегодно",
   "ежедневно", "ежемесячно", "еле", "еле-еле", "естественно", "ехидно", "еще", "жадно", "жалко", "жалобно", "жарко", "желательно", "жестко", "жестоко", "живо", "живьем",
   "жизненно", "жутко", "забавно", "заблаговременно", "заботливо", "заведомо", "завтра", "загадочно", "загодя", "задолго", "задумчиво", "законно", "законодательно",
   "заметно", "замечательно", "замуж", "замужем", "заново", "заодно", "заочно", "запросто", "заранее", "застенчиво", "затем", "зачастую", "звонко", "здорово", "зло",
   "злобно", "значительно", "зря", "зябко", "идеально", "идейно", "известно", "извне", "издавна", "издалека", "издали", "изначально", "изнутри", "изредка", "изрядно",
   "изумленно", "изящно", "именно", "иначе", "иногда", "инстинктивно", "интенсивно", "интересно", "интуитивно", "исключительно", "искоса", "искренне", "искусно",
   "искусственно", "исподволь", "исподлобья", "испокон", "исправно", "испуганно", "истинно", "исторически", "итого", "как-никак", "каково", "категорически", "качественно",
   "кверху", "конкретно", "коротко", "косвенно", "косо", "крайне", "красиво", "кратко", "крепко", "критически", "круглосуточно", "кругом", "крупно", "круто", "кстати",
   "культурно", "ладно", "ласково", "легко", "легонько", "лежа", "лениво", "лень", "лихо", "лихорадочно", "лично", "ловко", "логически", "логично", "лукаво", "любезно",
   "любовно", "любопытно", "максимально", "маленько", "мало", "маловато", "мало-мальски", "мало-помалу", "мастерски", "материально", "мгновенно", "медленно", "мелко",
   "мельком", "менее", "механически", "мигом", "мило", "мимо", "мимоходом", "мирно", "многозначительно", "многократно", "молниеносно", "молча", "молчаливо", "моментально",
   "морально", "мощно", "мрачно", "мужественно", "музыкально", "мучительно", "мысленно", "мягко", "набок", "навеки", "наверх", "наверху", "навечно", "навсегда", "навстречу",
   "нагло", "наглухо", "наглядно", "наголо", "наготове", "надвое", "надежно", "надобно", "надолго", "наедине", "назавтра", "назад", "назло", "наиболее", "наивно",
   "наизнанку", "наизусть", "наименее", "накануне", "налево", "налицо", "намеренно", "намертво", "намного", "наотрез", "наперебой", "наперед", "напоказ", "наполовину",
   "напоследок", "направо", "напрасно", "напролет", "напрочь", "напряженно", "напрямую", "наравне", "нарочито", "нарочно", "наружу", "наряду", "насильно", "насквозь",
   "насколько", "наскоро", "насмерть", "насмешливо", "насовсем", "наспех", "настежь", "настойчиво", "настолько", "настороженно", "настоятельно", "натурально", "наугад",
   "наутро", "научно", "национально", "начисто", "наяву", "небрежно", "неважно", "невдалеке", "неверно", "невероятно", "невесело", "невзирая", "невзначай", "невнятно",
   "невозможно", "невозмутимо", "невольно", "невпопад", "негативно", "негде", "негромко", "недавно", "недалеко", "недаром", "недоверчиво", "недовольно", "недолго",
   "недорого", "недостаточно", "недоуменно", "неестественно", "нежно", "независимо", "незадолго", "незаконно", "незамедлительно", "незаметно", "незачем", "неизбежно",
   "неизвестно", "неизменно", "некого", "некстати", "некуда", "нелегко", "нелепо", "неловко", "немало", "немедленно", "немедля", "неминуемо", "немного", "немножечко",
   "немножко", "ненадолго", "ненароком", "необычайно", "необычно", "необязательно", "неоднократно", "неожиданно", "неохота", "неохотно", "неплохо", "неподалеку",
   "неподвижно", "непонятно", "непосредственно", "неправильно", "непременно", "непрерывно", "неприятно", "непросто", "нервно", "нередко", "несколько", "неслышно",
   "неспешно", "неспроста", "несравненно", "нестерпимо", "нетерпеливо", "неторопливо", "нетрудно", "неуверенно", "неудачно", "неудержимо", "неудобно", "неуклонно",
   "неуклюже", "неумело", "неумолимо", "неуютно", "нехорошо", "нехотя", "нечасто", "нечаянно", "нечего", "нещадно", "неясно", "ниже", "низко", "нисколько", "ничего",
   "нормально", "ныне", "нынче", "обидно", "обиженно", "обильно", "облегченно", "образно", "обратно", "обреченно", "обстоятельно", "объективно", "обыкновенно", "обычно",
   "обязательно", "одинаково", "одиноко", "однажды", "одновременно", "однозначно", "одобрительно", "оживленно", "озабоченно", "около", "окончательно", "округ", "опасно",
   "оперативно", "определенно", "опять", "опять-таки", "организационно", "органически", "органично", "ослепительно", "основательно", "особенно", "особо", "осознанно",
   "осторожно", "остро", "отвратительно", "отдаленно", "отдельно", "откровенно", "открыто", "отлично", "относительно", "отныне", "отнюдь", "отрицательно", "отрывисто",
   "отчасти", "отчаянно", "отчетливо", "официально", "охотно", "очевидно", "очень", "ошибочно", "ощутимо", "параллельно", "паче", "первоначально", "периодически",
   "печально", "пешком", "письменно", "плавно", "плотно", "плохо", "по-английски", "поблизости", "по-вашему", "поверх", "повсеместно", "повсюду", "повторно", "поголовно",
   "подавно", "по-детски", "подле", "подлинно", "подобно", "подозрительно", "подолгу", "подробно", "по-другому", "подряд", "подчас", "позавчера", "позади", "позднее",
   "поздно", "по-иному", "поистине", "пока", "покорно", "покуда", "полезно", "политически", "полно", "полностью", "положительно", "помаленьку", "по-моему", "понаслышке",
   "по-настоящему", "поначалу", "по-нашему", "поневоле", "по-немецки", "понемногу", "понимающе", "по-новому", "поныне", "понятно", "поодаль", "поодиночке", "поочередно",
   "поперек", "пополам", "по-прежнему", "попросту", "попутно", "пора", "поразительно", "по-разному", "поровну", "порой", "порою", "по-русски", "порядком", "по-своему",
   "посему", "посередине", "после", "последовательно", "послезавтра", "послушно", "поспешно", "посреди", "посредине", "по-старому", "постепенно", "постоянно", "по-твоему",
   "потенциально", "потихонечку", "потихоньку", "потрясающе", "поутру", "по-французски", "похоже", "по-хозяйски", "по-хорошему", "по-человечески", "почти", "почтительно",
   "правильно", "практически", "превосходно", "предварительно", "предельно", "предостаточно", "предположительно", "прежде", "презрительно", "преимущественно", "прекрасно",
   "приблизительно", "приветливо", "привычно", "прилично", "применительно", "примерно", "примечательно", "принципиально", "пристально", "причем", "приятно", "произвольно",
   "пронзительно", "пропорционально", "просто", "простодушно", "просто-напросто", "противно", "профессионально", "прочно", "прочь", "прямиком", "прямо", "прямо-таки",
   "психически", "психологически", "публично", "пусто", "равно", "равнодушно", "равномерно", "радикально", "радостно", "раз", "раздраженно", "разом", "разочарованно",
   "разумно", "ранее", "рано", "рассеянно", "растерянно", "реально", "ревниво", "регулярно", "редко", "резко", "резонно", "решительно", "робко", "рядом", "рядышком",
   "самолично", "самостоятельно", "сбоку", "сверху", "светло", "свободно", "своевременно", "свыше", "сдержанно", "сдуру", "сегодня", "сейчас", "секретно", "сердечно",
   "сердито", "серьезно", "сзади", "сильно", "систематически", "скверно", "скептически", "скок", "сколько", "сколько-нибудь", "сколько-то", "скорее", "скоро", "скромно",
   "скупо", "скучно", "слабо", "славно", "сладко", "слева", "слегка", "след", "следом", "слепо", "слишком", "сложно", "слыхать", "слышно", "смело", "смертельно", "смешно",
   "смиренно", "смирно", "смутно", "смущенно", "снаружи", "сначала", "снизу", "снисходительно", "снова", "собственноручно", "совершенно", "совестно", "совместно", "совсем",
   "согласно", "сознательно", "сокрушенно", "солидно", "сонно", "сообща", "соответственно", "сосредоточенно", "сочувственно", "сперва", "спереди", "специально", "спешно",
   "сплошь", "спокойно", "сполна", "справа", "справедливо", "сравнительно", "сразу", "средне", "сродни", "сроду", "срочно", "стабильно", "столько", "стоя", "странно",
   "страстно", "страсть", "стратегически", "страх", "страшно", "стремительно", "строго", "стыдно", "сугубо", "судорожно", "сурово", "сухо", "существенно", "счастливо",
   "тайком", "тайно", "таинственно", "также", "твердо", "творчески", "темно", "теоретически", "теперь", "тепло", "терпеливо", "тесно", "технически", "типично", "тихо",
   "тихонечко", "тихонько", "тоже", "толком", "только", "только-только", "тонко", "торжественно", "торжествующе", "торопливо", "тоскливо", "тотчас", "точно", "тошно",
   "трагически", "традиционно", "тревожно", "трезво", "трижды", "трудно", "туго", "туда-сюда", "тупо", "тускло", "тщательно", "тщетно", "тяжело", "тяжко", "убедительно",
   "убежденно", "уверенно", "увлеченно", "угодно", "угрожающе", "угрюмо", "удачно", "удивительно", "удивленно", "удобно", "удовлетворенно", "ужасно", "уже", "уклончиво",
   "укоризненно", "украдкой", "умело", "умеренно", "умышленно", "уныло", "упорно", "упрямо", "усердно", "усиленно", "условно", "успешно", "устало", "уютно", "факультативно",
   "фактически", "фамильярно", "фанатично", "фантастически", "фатовато", "феерично", "фешенебельно", "фигурально", "физически", "фиктивно", "философски", "формально",
   "характерно", "хитро", "хладнокровно", "хмуро", "холодно", "хорошенько", "хорошо", "хрипло", "худо", "художественно", "целиком", "цепко", "целомудренно", "церемонно",
   "цивилизованно", "циклично", "цинически", "частенько", "частично", "часто", "чересчур", "чертовски", "честно", "четко", "чинно", "чисто", "чрезвычайно", "чрезмерно",
   "чудесно", "чудовищно", "чутко", "чуточку", "чуть", "чуть-чуть", "шепотом", "шибко", "широко", "шумно", "шутливо", "щадяще", "щеголевато", "щедро", "щепетильно",
   "экологически", "экономически", "элегантно", "элементарно", "энергично", "эффектно", "юзом", "юно", "юношески", "юмористически", "юридически", "явно", "явственно",
   "ярко", "яростно", "ясно", "не",
      //МЕСТОИМЕНИЯ
   "я", "мы", "ты", "вы", "он", "она", "оно", "они", "себя", "мой", "твой", "ваш", "наш", "свой", "его", "ее", "их", "то", "это", "тот", "этот", "такой", "таков",
   "столько", "весь", "всякий", "сам", "самый", "каждый", "любой", "иной", "другой", "кто? что?", "какой?", "каков?", "чей?", "сколько?", "кто", "что", "какой",
   "каков", "чей", "сколько", "никто", "ничто", "некого", "нечего", "никакой", "ничей", "нисколько", "кто-то", "кое-кто", "кто-нибудь", "кто-либо", "что-то", "кое-что",
   "что-нибудь", "что-либо", "какой-то", "какой-либо", "какой-нибудь", "некто", "нечто", "некоторый", "некий", "некоторая", "некоторые", "того", "нас", "", " "

  };

        public string Keywords(IFormFile docFile)
        {

            var files = GetFiles();
            Dictionary<string, int> keywordsRepeatByFileName = new Dictionary<string, int>();
            if (docFile != null && docFile.Length > 0)
            {
                //string pathToDocFile = Path.Combine(_pathToImages
                //                                   , Path.GetFileName(docFile.FileName));
                //_AddFiles(docFile);
                //docFile.SaveAs(pathToDocFile);
                AddDocsToDatabase();
                var database = GetDatabaseAsDictionary();

                foreach (var fileName in files)
                {
                    int counter = 0;
                    if (fileName != docFile.FileName)
                    {
                        foreach (var word_1 in database[fileName].Split(','))
                        {
                            foreach (var word_2 in database[docFile.FileName].Split(','))
                            {
                                if (word_1 == word_2)
                                    counter++;
                            }
                        }
                        keywordsRepeatByFileName.Add(fileName, counter);
                    }
                }
            }

            string resultDocName = "";
            foreach (var fileName in keywordsRepeatByFileName.Keys)
            {
                if (keywordsRepeatByFileName[fileName] >= 10)
                    resultDocName += fileName + "\n";

                //keywordsRepeatByFileName.FirstOrDefault(x => x.Value == keywordsRepeatByFileName.Values.OrderByDescending(i => i).First()).Key;
            }

            return resultDocName;
        }

        public List<string> _AddFiles(List<IFormFile> addedFiles)
        {
            List<string> filelist = new List<string>();
            if (addedFiles.Count > 0)
            {

                foreach (var file in addedFiles)
                {
                    string filename = file.FileName;
                    filename = Path.GetFileName(filename);
                    string extension = Path.GetExtension(filename);
                    string namefile = Path.GetFileNameWithoutExtension(filename);
                    string[] time = DateTime.Now.TimeOfDay.ToString().Split('.');
                    string newtime = time[0].Replace(":", "-");
                    string date = DateTime.Now.Date.ToString("yyyy-MM-dd") + "-" + newtime + "-";
                    string newFileName = date + namefile + extension;
                    string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//UserGOSTFiles", newFileName);
                    var stream = new FileStream(uploadpath, FileMode.Create);
                    file.CopyToAsync(stream);
                    filelist.Add(newFileName);
                }
            }
            return filelist;
        }

        public Dictionary<string, int> GetRepeatingWords(string pathToFile, int repeatsCount) => GetRepeatingWords(GetWordsListStatic(pathToFile), repeatsCount);
        public Dictionary<string, int> GetRepeatingWords(List<string> words, int repeatsCount)
        {
            Dictionary<string, int> repeatingWords = _getFilteredDictionary(words);

            //Заполнение словаря из слов, которые повторились <repeatsCount> и более раз
            Dictionary<string, int> resultWords = new Dictionary<string, int>();
            for (int i = 0; i < repeatingWords.Count; i++)
                if (repeatingWords.Values.ElementAt(i) >= repeatsCount)
                    resultWords.Add(repeatingWords.Keys.ElementAt(i), repeatingWords.Values.ElementAt(i));

            return resultWords;
        }

        private static Dictionary<string, int> _getFilteredDictionary(List<string> words)
        {
            Dictionary<string, int> repeatingWords = new Dictionary<string, int>();

            //Фильтр слов и добавление в словарь
            foreach (string slovo in words)
            {
                string tword = slovo.Trim().ToLower();
                string word = string.Empty;
                for (int i = 0; i < tword.Length; i++)
                    if (char.IsLetter(tword[i]))
                        word += tword[i].ToString();

                if (_stopWords.Contains(word))
                    continue;

                if (repeatingWords.ContainsKey(word))
                {
                    repeatingWords[word]++;
                }
                else
                {
                    repeatingWords.Add(word, 1);
                }
            }

            return repeatingWords;
        }

        public static List<string> GetWordsListStatic(string pathToFile)
        {
            List<string> slova = new List<string>();

            if (Path.GetExtension(pathToFile) == ".docx")
            {
                //Load Document
                Document document = new Document();
                document.LoadFromFile(pathToFile);

                //Initialzie StringBuilder Instance
                StringBuilder sb = new StringBuilder();


                //Extract Text from Word and Save to StringBuilder Instance
                foreach (Section section in document.Sections)
                    foreach (Paragraph paragraph in section.Paragraphs)
                        sb.AppendLine(paragraph.Text);

                slova = sb.ToString().Split(' ').ToList();
            }
            else if (Path.GetExtension(pathToFile) == ".txt")
                slova = File.ReadAllText(pathToFile).Split(' ').ToList();

            return slova;
        }

        private List<string> GetFiles()
        {
            var dir = new DirectoryInfo(_pathToImages);
            //FileInfo[] files = dir.GetFiles("*.*");
            var extensions = new string[] { "*.doc", "*.docx", "*.pdf" };
            var files = extensions.SelectMany(ext => dir.GetFiles(ext)).ToArray();

            return files.Select(file => file.Name).Distinct().ToList();
        }

        public Dictionary<string, string> GetDatabaseAsDictionary()
        {
            string databasePath = Path.Combine(_pathToImages, "CurrentDocs.yerlan");
            Dictionary<string, string> resultDict = new Dictionary<string, string>();
            var fileLines = System.IO.File.ReadAllLines(databasePath);

            foreach (var line in fileLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                    resultDict.Add(line.Split(':')[0], line.Split(':')[1]);
            }

            return resultDict;
        }

        public void AddDocsToDatabase()
        {
            var files = GetFiles();
            var documentRepository = new DocumentRepository();
            string databasePath = Path.Combine(_pathToImages, "CurrentDocs.yerlan");
            var fileNamesInDatabase = System.IO.File.ReadAllLines(databasePath).Select(line => line.Split(':').First()).ToArray();

            foreach (var fileName in files)
            {
                string filePath = $"{_pathToImages}/{fileName}";
                var repeatingWords = documentRepository.GetRepeatingWords(filePath, 3);

                if (!fileNamesInDatabase.Contains(fileName))
                    System.IO.File.AppendAllText(databasePath, $"{fileName}:{string.Join(",", repeatingWords.Where(x => repeatingWords.Values.OrderByDescending(i => i).Take(50).Contains(x.Value)).Select(p => p.Key).ToArray())}\n");
            }

            var lines = System.IO.File.ReadAllLines(databasePath).Where(line => !string.IsNullOrWhiteSpace(line));
            System.IO.File.WriteAllLines(databasePath, lines);

        }

        public async Task<List<GOST>> CreateGOST(string userName, [FromForm] GostFormData gost)
        {
            List<GOST> result = new List<GOST>();
            var user = await _db.Users.FirstOrDefaultAsync(r => r.Username == userName);
            List<UsersGosts> userGOSTS = new List<UsersGosts>();
            List<string> gostFiles = new List<string>();
            if (gost.Gost.Count > 0)
            {
                gostFiles = _AddFiles(gost.Gost);
            }
            for (int i = 0; i < gostFiles.Count; i++)
            {
                userGOSTS.Add(new UsersGosts
                {
                    gostName = gostFiles[i],
                });
            }
            var newGost = new GOST
            {
                userId = user.Id,
                UsersGosts = userGOSTS,
                OnWork = false,
                isFinished = false
            };
            await _db.GOST.AddAsync(newGost);
            result.Add(newGost);
            await _db.SaveChangesAsync();
            return result;
        }
    }
}
