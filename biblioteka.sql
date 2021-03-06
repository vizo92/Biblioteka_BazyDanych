USE [master]
GO
/****** Object:  Database [biblioteka]    Script Date: 08.05.2019 21:15:07 ******/
CREATE DATABASE [biblioteka]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'biblioteka', FILENAME = N'C:\Users\skwia\biblioteka.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'biblioteka_log', FILENAME = N'C:\Users\skwia\biblioteka_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO
ALTER DATABASE [biblioteka] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [biblioteka].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [biblioteka] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [biblioteka] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [biblioteka] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [biblioteka] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [biblioteka] SET ARITHABORT OFF 
GO
ALTER DATABASE [biblioteka] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [biblioteka] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [biblioteka] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [biblioteka] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [biblioteka] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [biblioteka] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [biblioteka] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [biblioteka] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [biblioteka] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [biblioteka] SET  DISABLE_BROKER 
GO
ALTER DATABASE [biblioteka] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [biblioteka] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [biblioteka] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [biblioteka] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [biblioteka] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [biblioteka] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [biblioteka] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [biblioteka] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [biblioteka] SET  MULTI_USER 
GO
ALTER DATABASE [biblioteka] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [biblioteka] SET DB_CHAINING OFF 
GO
ALTER DATABASE [biblioteka] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [biblioteka] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [biblioteka] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [biblioteka] SET QUERY_STORE = OFF
GO
USE [biblioteka]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [biblioteka]
GO
/****** Object:  Table [dbo].[autorzy]    Script Date: 08.05.2019 21:15:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[autorzy](
	[id_autora] [int] IDENTITY(1,1) NOT NULL,
	[imie] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[nazwisko] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[narodowosc] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[liczba_dziel] [int] NULL,
	[zyciorys] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_autorzy] PRIMARY KEY CLUSTERED 
(
	[id_autora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[czytelnicy]    Script Date: 08.05.2019 21:15:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[czytelnicy](
	[id_czytelnika] [int] IDENTITY(1,1) NOT NULL,
	[imie] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[nazwisko] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[miastso] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ulica] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[liczba_ksiazek] [int] NULL,
 CONSTRAINT [PK_czytelnicy] PRIMARY KEY CLUSTERED 
(
	[id_czytelnika] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[gatunki]    Script Date: 08.05.2019 21:15:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[gatunki](
	[nazwa] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_gatunki] PRIMARY KEY CLUSTERED 
(
	[nazwa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ksiazki]    Script Date: 08.05.2019 21:15:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ksiazki](
	[id_ksiazki] [int] IDENTITY(1,1) NOT NULL,
	[id_autora] [int] NULL,
	[wydawnictwo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[gatunek] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[tytul] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_ksiazki] PRIMARY KEY CLUSTERED 
(
	[id_ksiazki] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[wydawnictwa]    Script Date: 08.05.2019 21:15:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wydawnictwa](
	[nazwa] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[kraj] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[miasto] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_wydawnictwa] PRIMARY KEY CLUSTERED 
(
	[nazwa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[wypozyczenia]    Script Date: 08.05.2019 21:15:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wypozyczenia](
	[id_wypozyczenia] [int] IDENTITY(1,1) NOT NULL,
	[id_czytelnika] [int] NULL,
	[id_ksiazki] [int] NULL,
	[data_zamowienia] [date] NULL,
	[data_wypozyczenia] [date] NULL,
	[data_zwrotu] [date] NULL,
	[status] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_wypozyczenia] PRIMARY KEY CLUSTERED 
(
	[id_wypozyczenia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[autorzy] ON 

INSERT [dbo].[autorzy] ([id_autora], [imie], [nazwisko], [narodowosc], [liczba_dziel], [zyciorys]) VALUES (1, N'Jan', N'Kochanowski', N'polska', 3, N'Jan Kochanowski (ur. 1530 w Sycynie, zm. 22 sierpnia 1584 w Lublinie) – polski poeta epoki renesansu, tlumacz, prepozyt kapituly katedralnej poznanskiej w latach 1564–1574[3], poeta nadworny Stefana Batorego w 1579 roku[4], sekretarz królewski i wojski sandomierski w latach 1579–1584[5]. Uwazany jest za jednego z najwybitniejszych twórców renesansu w Europie i poete, który najbardziej przyczynil sie do rozwoju polskiego jezyka literackiego.')
INSERT [dbo].[autorzy] ([id_autora], [imie], [nazwisko], [narodowosc], [liczba_dziel], [zyciorys]) VALUES (2, N'Adam', N'Mickiewicz', N'polska', 2, N'Adam Bernard Mickiewicz (ur. 24 grudnia 1798 w Zaosiu lub Nowogródku[1][2][3], zm. 26 listopada 1855 w Stambule) – polski poeta, dzialacz polityczny, publicysta, tlumacz, filozof, dzialacz religijny, mistyk, organizator i dowódca wojskowy, nauczyciel akademicki.')
INSERT [dbo].[autorzy] ([id_autora], [imie], [nazwisko], [narodowosc], [liczba_dziel], [zyciorys]) VALUES (3, N'Juliusz', N'Slowacki', N'polska', 2, N'Juliusz Slowacki herbu Leliwa (ur. 4 wrzesnia 1809 w Krzemiencu, zm. 3 kwietnia 1849 w Paryzu[1]) – polski poeta, przedstawiciel romantyzmu, dramaturg i epistolograf. Obok Mickiewicza i Krasinskiego okreslany jako jeden z Wieszczów Narodowych. Twórca filozofii genezyjskiej (pneumatycznej), epizodycznie zwiazany takze z mesjanizmem polskim, byl tez mistykiem. Obok Mickiewicza uznawany powszechnie za najwiekszego przedstawiciela polskiego romantyzmu.')
INSERT [dbo].[autorzy] ([id_autora], [imie], [nazwisko], [narodowosc], [liczba_dziel], [zyciorys]) VALUES (4, N'Henryk', N'Sienkiewicz', N'polska', 4, N'Henryk Adam Aleksander Pius Sienkiewicz herbu Oszyk, kryptonim „Litwos”, „Musagetes”, pseudonim „Juliusz Polkowski”, „K. Dobrzynski” (ur. 5 maja 1846 w Woli Okrzejskiej, zm. 15 listopada 1916 w Vevey) – polski nowelista, powiesciopisarz i publicysta; laureat Nagrody Nobla w dziedzinie literatury (1905) za caloksztalt twórczosci[1], jeden z najpopularniejszych polskich pisarzy przelomu XIX i XX w.')
INSERT [dbo].[autorzy] ([id_autora], [imie], [nazwisko], [narodowosc], [liczba_dziel], [zyciorys]) VALUES (5, N'Homer', NULL, N'grecka', 2, N'Homer (stgr. ?µ????, Hómeros, gr. ?µ????) (VIII wiek p.n.e.) – grecki piesniarz wedrowny (aojda), epik, spiewak i recytator (rapsod). Uwaza sie go za ojca poezji epickiej. Najstarszy znany z imienia europejski poeta, który zapewne przejal dziedzictwo dlugiej i bogatej tradycji ustnej poezji heroicznej. Do jego dziel zalicza sie eposy: Iliade i Odyseje. Grecka tradycja widziala w nim równiez autora poematów heroikomicznych Batrachomyomachia i Margites oraz Hymnów homeryckich. Zaden poeta grecki nie przewyzszyl slawa Homera. Na wyspach Ios i Chios wzniesiono poswiecone mu swiatynie, a w Olimpii i Delfach postawiono jego posagi. Pizystrat wprowadzil recytacje homeryckich poematów na Panatenajach.')
INSERT [dbo].[autorzy] ([id_autora], [imie], [nazwisko], [narodowosc], [liczba_dziel], [zyciorys]) VALUES (6, N'Franz', N'Kafka', N'niemiecka', 2, N'Franz Kafka (ur. 3 lipca 1883 w Pradze, zm. 3 czerwca 1924 w Kierling) – niemieckojezyczny pisarz pochodzenia zydowskiego, przez cale zycie zwiazany z Praga. W swoich powiesciach stworzyl model sytuacji zwanej sytuacja kafkowska i okreslanej w jezyku niemieckim za pomoca przymiotnika „kafkaesk”, którego istota jest konflikt zniewolonej jednostki z anonimowa, nadrzedna wobec niej instancja. Deformacja groteskowa, niejednoznaczne, paraboliczne obrazy oraz poczucie zagrozenia i niepewnosci skladaja sie na panorame literackiego swiata Kafki.')
INSERT [dbo].[autorzy] ([id_autora], [imie], [nazwisko], [narodowosc], [liczba_dziel], [zyciorys]) VALUES (7, N'Fiodor', N'Dostojewski', N'rosyjska', 2, N'Fiodor Michajlowicz Dostojewski (ros. ????? ?????????? ???????????; ur. 30 pazdziernika?/11 listopada 1821 w Moskwie, Imperium Rosyjskie, zm. 28 stycznia?/9 lutego 1881 w Petersburgu, Imperium Rosyjskie) – rosyjski pisarz i mysliciel. Jeden z najbardziej wplywowych powiesciopisarzy literatury rosyjskiej i swiatowej.')
INSERT [dbo].[autorzy] ([id_autora], [imie], [nazwisko], [narodowosc], [liczba_dziel], [zyciorys]) VALUES (8, N'William', N'Shakespeare', N'angielska', 3, N'William Shakespeare (Szekspir) (ur. prawdopodobnie 23 kwietnia 1564, data chrztu: 26 kwietnia 1564[2], w Stratford-upon-Avon, zm. 23 kwietnia?/3 maja 1616 tamze) – angielski poeta, dramaturg, aktor. Powszechnie uwazany za jednego z najwybitniejszych pisarzy literatury angielskiej oraz reformatorów teatru[3].')
INSERT [dbo].[autorzy] ([id_autora], [imie], [nazwisko], [narodowosc], [liczba_dziel], [zyciorys]) VALUES (9, N'Dante', N'Alighieri', N'wloska', 2, N'Dante Alighieri (ur. w maju lub czerwcu 1265 we Florencji, zm. 13 lub 14 wrzesnia 1321 w Rawennie) – wloski poeta, filozof i polityk.  Jego najwybitniejszym dzielem jest poemat Boska komedia, uwazany za arcydzielo literatury swiatowej i szczytowe osiagniecie literatury sredniowiecznej. Utwór jest w calosci napisany po wlosku.')
INSERT [dbo].[autorzy] ([id_autora], [imie], [nazwisko], [narodowosc], [liczba_dziel], [zyciorys]) VALUES (10, N'Geoffrey', N'Chaucer', N'angielska', 2, N'Geoffrey Chaucer (ur. ok. 1343, mozliwe ze w Londynie, ale dokladniejsza data i miejsce nie sa znane, zm. 25 pazdziernika 1400) – angielski poeta, filozof i dyplomata.')
INSERT [dbo].[autorzy] ([id_autora], [imie], [nazwisko], [narodowosc], [liczba_dziel], [zyciorys]) VALUES (11, N'Sebastian', N'Fitzek', N'niemiecka', 0, N'Niemiecki pisarz i dziennikarz. Studiowal prawo, specjalizowal sie w prawie autorskim. Pracuje w berlinskiej rozglosni 104.6 RTL.
Od 2006 roku publikuje thrillery. Jego ksiazki przetlumaczono na 18 jezyków; jest jednym z nielicznych niemieckich autorów kryminalów wydawanym w USA i Wielkiej Brytanii.')
INSERT [dbo].[autorzy] ([id_autora], [imie], [nazwisko], [narodowosc], [liczba_dziel], [zyciorys]) VALUES (2011, N'Jeremy', N'Clarkson', N'angielska', 0, N'Jeremy Charles Robert Clarkson (ur. 11 kwietnia 1960 w Doncaster) – angielski dziennikarz telewizyjny, zajmujacy sie motoryzacja, a takze publicysta i podróznik, najbardziej znany jako prezenter programu Top Gear.')
SET IDENTITY_INSERT [dbo].[autorzy] OFF
SET IDENTITY_INSERT [dbo].[czytelnicy] ON 

INSERT [dbo].[czytelnicy] ([id_czytelnika], [imie], [nazwisko], [miastso], [ulica], [liczba_ksiazek]) VALUES (1, N'Adam', N'Malysz', N'Warszawa', N'Grójecka 1', 1)
INSERT [dbo].[czytelnicy] ([id_czytelnika], [imie], [nazwisko], [miastso], [ulica], [liczba_ksiazek]) VALUES (2, N'Robert', N'Mateja', N'Kraków', N'Warszawska 2', 1)
INSERT [dbo].[czytelnicy] ([id_czytelnika], [imie], [nazwisko], [miastso], [ulica], [liczba_ksiazek]) VALUES (3, N'Adam', N'Kowalski', N'Warszawa', N'Lucka 1', 1)
INSERT [dbo].[czytelnicy] ([id_czytelnika], [imie], [nazwisko], [miastso], [ulica], [liczba_ksiazek]) VALUES (4, N'Mariusz', N'Pudzianowski', N'Biala Rawska', N'Rawska 3', 1)
INSERT [dbo].[czytelnicy] ([id_czytelnika], [imie], [nazwisko], [miastso], [ulica], [liczba_ksiazek]) VALUES (1004, N'Ignacy', N'Michaluk', N'Warszawa', N'Jerozolimskie 14', 0)
SET IDENTITY_INSERT [dbo].[czytelnicy] OFF
INSERT [dbo].[gatunki] ([nazwa]) VALUES (N'Dramat')
INSERT [dbo].[gatunki] ([nazwa]) VALUES (N'Epika')
INSERT [dbo].[gatunki] ([nazwa]) VALUES (N'Fantastyka')
INSERT [dbo].[gatunki] ([nazwa]) VALUES (N'Kryminal')
INSERT [dbo].[gatunki] ([nazwa]) VALUES (N'Poezja')
INSERT [dbo].[gatunki] ([nazwa]) VALUES (N'Sci-Fi')
SET IDENTITY_INSERT [dbo].[ksiazki] ON 

INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (1, 1, N'Dragon', N'Poezja', N'Fraszki, piesni, treny. Dziela wybrane')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (2, 1, N'Nowa Era', N'Epika', N'Odprawa poslów greckich')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (3, 2, N'Nowa Era', N'Epika', N'Pan Tadeusz')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (4, 2, N'Swiat Ksiazki', N'Poezja', N'Sonety Krymskie')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (5, 3, N'Swiat Ksiazki', N'Dramat', N'Balladyna')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (6, 3, N'WSiP', N'Dramat', N'Kordian')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (7, 4, N'WSiP', N'Epika', N'Quo Vadis')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (8, 4, N'WSiP', N'Epika', N'Krzyzacy')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (9, 4, N'WSiP', N'Epika', N'Ogniem i Mieczem')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (10, 4, N'Nowa Era', N'Dramat', N'Janko Muzykant')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (11, 5, N'Dragon', N'Dramat', N'Odyseja')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (12, 5, N'Reader''s Digest', N'Dramat', N'Iliada')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (13, 6, N'Dragon', N'Epika', N'Ameryka')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (14, 6, N'Reader''s Digest', N'Epika', N'Aforyzmy z Zürau')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (15, 7, N'Reader''s Digest', N'Kryminal', N'Aforyzmy')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (16, 7, N'Sowa', N'Kryminal', N'Biale noce')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (17, 8, N'Swiat Ksiazki', N'Dramat', N'Makbet')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (18, 8, N'Swiat Ksiazki', N'Dramat', N'Hamlet')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (19, 8, N'Swiat Ksiazki', N'Dramat', N'Romeo i Julia')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (20, 9, N'Nowa Era', N'Dramat', N'Boska Komedia')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (21, 9, N'Sowa', N'Dramat', N'Inferno')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (22, 10, N'Sowa', N'Fantastyka', N'Opowiesci kanterberyjskie')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (23, 10, N'Reader''s Digest', N'Sci-Fi', N'Opowiesc Rycerza')
INSERT [dbo].[ksiazki] ([id_ksiazki], [id_autora], [wydawnictwo], [gatunek], [tytul]) VALUES (1002, 1, N'Reader''s Digest', N'Poezja', N'Treny')
SET IDENTITY_INSERT [dbo].[ksiazki] OFF
INSERT [dbo].[wydawnictwa] ([nazwa], [kraj], [miasto]) VALUES (N'Dragon', N'USA', N'Washington')
INSERT [dbo].[wydawnictwa] ([nazwa], [kraj], [miasto]) VALUES (N'Nowa Era', N'Polska', N'Bialystok')
INSERT [dbo].[wydawnictwa] ([nazwa], [kraj], [miasto]) VALUES (N'Reader''s Digest', N'Anglia', N'London')
INSERT [dbo].[wydawnictwa] ([nazwa], [kraj], [miasto]) VALUES (N'Sowa', N'Polska', N'Wroclaw')
INSERT [dbo].[wydawnictwa] ([nazwa], [kraj], [miasto]) VALUES (N'Swiat Ksiazki', N'Polska', N'Kraków')
INSERT [dbo].[wydawnictwa] ([nazwa], [kraj], [miasto]) VALUES (N'WSiP', N'Polska', N'Gdansk')
SET IDENTITY_INSERT [dbo].[wypozyczenia] ON 

INSERT [dbo].[wypozyczenia] ([id_wypozyczenia], [id_czytelnika], [id_ksiazki], [data_zamowienia], [data_wypozyczenia], [data_zwrotu], [status]) VALUES (1, 3, 7, CAST(N'2019-05-06' AS Date), NULL, NULL, N'Zarezerwowane')
INSERT [dbo].[wypozyczenia] ([id_wypozyczenia], [id_czytelnika], [id_ksiazki], [data_zamowienia], [data_wypozyczenia], [data_zwrotu], [status]) VALUES (2, 1, 3, CAST(N'2019-04-12' AS Date), CAST(N'2019-04-14' AS Date), NULL, N'Wypozyczone')
INSERT [dbo].[wypozyczenia] ([id_wypozyczenia], [id_czytelnika], [id_ksiazki], [data_zamowienia], [data_wypozyczenia], [data_zwrotu], [status]) VALUES (3, 2, 10, CAST(N'2019-04-03' AS Date), CAST(N'2019-04-05' AS Date), CAST(N'2019-04-28' AS Date), N'Zwrócone')
INSERT [dbo].[wypozyczenia] ([id_wypozyczenia], [id_czytelnika], [id_ksiazki], [data_zamowienia], [data_wypozyczenia], [data_zwrotu], [status]) VALUES (1002, 2, 21, CAST(N'2019-05-07' AS Date), NULL, NULL, N'Zarezerwowane')
INSERT [dbo].[wypozyczenia] ([id_wypozyczenia], [id_czytelnika], [id_ksiazki], [data_zamowienia], [data_wypozyczenia], [data_zwrotu], [status]) VALUES (1003, 1, 23, CAST(N'2019-04-02' AS Date), CAST(N'2019-05-01' AS Date), CAST(N'2019-05-07' AS Date), N'Zwrócone')
INSERT [dbo].[wypozyczenia] ([id_wypozyczenia], [id_czytelnika], [id_ksiazki], [data_zamowienia], [data_wypozyczenia], [data_zwrotu], [status]) VALUES (2002, 4, 23, CAST(N'2019-02-01' AS Date), CAST(N'2019-02-04' AS Date), CAST(N'2019-02-28' AS Date), N'Zwrócone')
INSERT [dbo].[wypozyczenia] ([id_wypozyczenia], [id_czytelnika], [id_ksiazki], [data_zamowienia], [data_wypozyczenia], [data_zwrotu], [status]) VALUES (2003, 4, 13, CAST(N'2019-05-06' AS Date), CAST(N'2019-05-06' AS Date), NULL, N'Wypozyczone')
INSERT [dbo].[wypozyczenia] ([id_wypozyczenia], [id_czytelnika], [id_ksiazki], [data_zamowienia], [data_wypozyczenia], [data_zwrotu], [status]) VALUES (2004, 3, 17, CAST(N'2019-05-02' AS Date), CAST(N'2019-05-03' AS Date), NULL, N'Wypozyczone')
INSERT [dbo].[wypozyczenia] ([id_wypozyczenia], [id_czytelnika], [id_ksiazki], [data_zamowienia], [data_wypozyczenia], [data_zwrotu], [status]) VALUES (2005, 2, 21, CAST(N'2019-04-11' AS Date), CAST(N'2019-04-15' AS Date), NULL, N'Wypozyczone')
SET IDENTITY_INSERT [dbo].[wypozyczenia] OFF
ALTER TABLE [dbo].[autorzy] ADD  CONSTRAINT [DF_autorzy_liczba_dziel]  DEFAULT ((0)) FOR [liczba_dziel]
GO
ALTER TABLE [dbo].[czytelnicy] ADD  CONSTRAINT [DF_czytelnicy_liczba_ksiazek]  DEFAULT ((0)) FOR [liczba_ksiazek]
GO
ALTER TABLE [dbo].[ksiazki]  WITH CHECK ADD  CONSTRAINT [FK_ksiazki_autorzy] FOREIGN KEY([id_autora])
REFERENCES [dbo].[autorzy] ([id_autora])
GO
ALTER TABLE [dbo].[ksiazki] CHECK CONSTRAINT [FK_ksiazki_autorzy]
GO
ALTER TABLE [dbo].[ksiazki]  WITH CHECK ADD  CONSTRAINT [FK_ksiazki_gatunki] FOREIGN KEY([gatunek])
REFERENCES [dbo].[gatunki] ([nazwa])
GO
ALTER TABLE [dbo].[ksiazki] CHECK CONSTRAINT [FK_ksiazki_gatunki]
GO
ALTER TABLE [dbo].[ksiazki]  WITH CHECK ADD  CONSTRAINT [FK_ksiazki_wydawnictwa] FOREIGN KEY([wydawnictwo])
REFERENCES [dbo].[wydawnictwa] ([nazwa])
GO
ALTER TABLE [dbo].[ksiazki] CHECK CONSTRAINT [FK_ksiazki_wydawnictwa]
GO
ALTER TABLE [dbo].[wypozyczenia]  WITH CHECK ADD  CONSTRAINT [FK_wypozyczenia_czytelnicy] FOREIGN KEY([id_czytelnika])
REFERENCES [dbo].[czytelnicy] ([id_czytelnika])
GO
ALTER TABLE [dbo].[wypozyczenia] CHECK CONSTRAINT [FK_wypozyczenia_czytelnicy]
GO
ALTER TABLE [dbo].[wypozyczenia]  WITH CHECK ADD  CONSTRAINT [FK_wypozyczenia_ksiazki] FOREIGN KEY([id_ksiazki])
REFERENCES [dbo].[ksiazki] ([id_ksiazki])
GO
ALTER TABLE [dbo].[wypozyczenia] CHECK CONSTRAINT [FK_wypozyczenia_ksiazki]
GO
USE [master]
GO
ALTER DATABASE [biblioteka] SET  READ_WRITE 
GO
