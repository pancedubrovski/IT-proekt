﻿DECLARE @CurrentMigration [nvarchar](max)

IF object_id('[dbo].[__MigrationHistory]') IS NOT NULL
    SELECT @CurrentMigration =
        (SELECT TOP (1) 
        [Project1].[MigrationId] AS [MigrationId]
        FROM ( SELECT 
        [Extent1].[MigrationId] AS [MigrationId]
        FROM [dbo].[__MigrationHistory] AS [Extent1]
        WHERE [Extent1].[ContextKey] = N'proekt.Migrations.Configuration'
        )  AS [Project1]
        ORDER BY [Project1].[MigrationId] DESC)

IF @CurrentMigration IS NULL
    SET @CurrentMigration = '0'

IF @CurrentMigration < '201909291229196_Inital'
BEGIN
    CREATE TABLE [dbo].[Cards] (
        [Recordid] [int] NOT NULL IDENTITY,
        [cardId] [nvarchar](max),
        [TelefonId] [int] NOT NULL,
        [Count] [int] NOT NULL,
        [DataCreate] [datetime] NOT NULL,
        CONSTRAINT [PK_dbo.Cards] PRIMARY KEY ([Recordid])
    )

    CREATE INDEX [IX_TelefonId] ON [dbo].[Cards]([TelefonId])
    CREATE TABLE [dbo].[Telefons] (
        [TelefonID] [int] NOT NULL IDENTITY,
        [ImeTelefon] [nvarchar](max) NOT NULL,
        [cena] [int] NOT NULL,
        [slika] [nvarchar](max),
        [proID] [int] NOT NULL,
        CONSTRAINT [PK_dbo.Telefons] PRIMARY KEY ([TelefonID])
    )

    CREATE INDEX [IX_proID] ON [dbo].[Telefons]([proID])
    CREATE TABLE [dbo].[Proizvoditels] (
        [proID] [int] NOT NULL IDENTITY,
        [ime] [nvarchar](max),
        CONSTRAINT [PK_dbo.Proizvoditels] PRIMARY KEY ([proID])
    )
    CREATE TABLE [dbo].[Orders] (
        [OrderId] [int] NOT NULL IDENTITY,
        [Username] [nvarchar](max),
        [FirstName] [nvarchar](max) NOT NULL,
        [LastName] [nvarchar](max) NOT NULL,
        [Address] [nvarchar](max) NOT NULL,
        [City] [nvarchar](max) NOT NULL,
        [State] [nvarchar](max) NOT NULL,
        [PostalCode] [nvarchar](max) NOT NULL,
        [Country] [nvarchar](max) NOT NULL,
        [Phone] [nvarchar](max) NOT NULL,
        [Email] [nvarchar](max) NOT NULL,
        [Total] [decimal](18, 2) NOT NULL,
        [OrderDate] [datetime] NOT NULL,
        CONSTRAINT [PK_dbo.Orders] PRIMARY KEY ([OrderId])
    )
    CREATE TABLE [dbo].[OrederDetals] (
        [OrderDetailId] [int] NOT NULL IDENTITY,
        [OrderId] [int] NOT NULL,
        [TelefonId] [int] NOT NULL,
        [Quantity] [int] NOT NULL,
        [UnitPrice] [decimal](18, 2) NOT NULL,
        CONSTRAINT [PK_dbo.OrederDetals] PRIMARY KEY ([OrderDetailId])
    )
    CREATE INDEX [IX_OrderId] ON [dbo].[OrederDetals]([OrderId])
    CREATE INDEX [IX_TelefonId] ON [dbo].[OrederDetals]([TelefonId])
    CREATE TABLE [dbo].[AspNetRoles] (
        [Id] [nvarchar](128) NOT NULL,
        [Name] [nvarchar](256) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY ([Id])
    )
    CREATE UNIQUE INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]([Name])
    CREATE TABLE [dbo].[AspNetUserRoles] (
        [UserId] [nvarchar](128) NOT NULL,
        [RoleId] [nvarchar](128) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId])
    )

    CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]([UserId])
    CREATE INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]([RoleId])
    CREATE TABLE [dbo].[AspNetUsers] (
        [Id] [nvarchar](128) NOT NULL,
        [Email] [nvarchar](256),
        [EmailConfirmed] [bit] NOT NULL,
        [PasswordHash] [nvarchar](max),
        [SecurityStamp] [nvarchar](max),
        [PhoneNumber] [nvarchar](max),
        [PhoneNumberConfirmed] [bit] NOT NULL,
        [TwoFactorEnabled] [bit] NOT NULL,
        [LockoutEndDateUtc] [datetime],
        [LockoutEnabled] [bit] NOT NULL,
        [AccessFailedCount] [int] NOT NULL,
        [UserName] [nvarchar](256) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY ([Id])
    )
    CREATE UNIQUE INDEX [UserNameIndex] ON [dbo].[AspNetUsers]([UserName])
    CREATE TABLE [dbo].[AspNetUserClaims] (
        [Id] [int] NOT NULL IDENTITY,
        [UserId] [nvarchar](128) NOT NULL,
        [ClaimType] [nvarchar](max),
        [ClaimValue] [nvarchar](max),
        CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY ([Id])
    )
    CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]([UserId])
    CREATE TABLE [dbo].[AspNetUserLogins] (
        [LoginProvider] [nvarchar](128) NOT NULL,
        [ProviderKey] [nvarchar](128) NOT NULL,
        [UserId] [nvarchar](128) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey], [UserId])
    )

    CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]([UserId])

    ALTER TABLE [dbo].[Cards] ADD CONSTRAINT [FK_dbo.Cards_dbo.Telefons_TelefonId] FOREIGN KEY ([TelefonId]) REFERENCES [dbo].[Telefons] ([TelefonID]) ON DELETE CASCADE
 
  ALTER TABLE [dbo].[Telefons] ADD CONSTRAINT [FK_dbo.Telefons_dbo.Proizvoditels_proID] FOREIGN KEY ([proID]) REFERENCES [dbo].[Proizvoditels] ([proID]) ON DELETE CASCADE
    ALTER TABLE [dbo].[OrederDetals] ADD CONSTRAINT [FK_dbo.OrederDetals_dbo.Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]) ON DELETE CASCADE
    ALTER TABLE [dbo].[OrederDetals] ADD CONSTRAINT [FK_dbo.OrederDetals_dbo.Telefons_TelefonId] FOREIGN KEY ([TelefonId]) REFERENCES [dbo].[Telefons] ([TelefonID]) ON DELETE CASCADE


 ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[AspNetUserClaims] ADD CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[AspNetUserLogins] ADD CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
  CREATE TABLE [dbo].[__MigrationHistory] (
        [MigrationId] [nvarchar](150) NOT NULL,
        [ContextKey] [nvarchar](300) NOT NULL,
        [Model] [varbinary](max) NOT NULL,
        [ProductVersion] [nvarchar](32) NOT NULL,
        CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
    )
    INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
    VALUES (N'201909291229196_Inital', N'proekt.Migrations.Configuration',  0x1F8B0800000000000400DD5DDB52E4B819BE4F55DEC1D557498AA507C84C4DA866B7D86648A80C874C335BB9A3842DC0353EF4DA6E1692DA27CB451E29AF10C9966D9D2DF9CC1637B42C7D927E7D3AFD923FFFEF3FFF5DFDF01206CE334C523F8E4E1607FBEF160E8CDCD8F3A3C793C52E7BF8EEE3E287EF7FFFBBD5272F7C717E2AE31DE1782865949E2C9EB26C7BBC5CA6EE130C41BA1FFA6E12A7F143B6EFC6E11278F1F2F0DDBBBF2C0F0E9610412C1096E3ACBEECA2CC0F61FE03FD5CC7910BB7D90E0497B107839484A3279B1CD5B902214CB7C085278B6D12C36FD97E1171E19C063E4085D8C0E061E180288A3390A1221E7F4DE1264BE2E871B3450120B87DDD4214EF01042924453FAEA39BD6E2DD21AEC5B24E5842B9BB348B434BC083236296259FBC95711795D990E13E210367AFB8D6B9F14E166B90780B87CFE8781D24381267D77D1C7BCF29C2F6AA7647F4C07F7BCE7A1764BB049E4470972520D8736E76F781EFFE1DBEDEC6DF607412ED82802E0E2A107AC604A0A09B24DEC2247BFD021F4821BF40374E3C1F1574C9A65EF2C9ABC442CAA23A17517674B870AE5041C07D00AB96A7AABEC9E204FE1546300119F46E4096C10435DC850773DB0965E0727491892EAAFC10D95097593897E0E5338C1EB3A79305FA77E19CFB2FD02B434819BE463EEA61285196EC605336B730800F7174D158333DCC3A465DAE1BC419C8C03A81C858250E0A81B7A81F37435D8167FF3137BBBC7A0BE70B0CF2E7E993BF2DFA734EC2BB2AC27912875FE28050B90CBFDBC4BBC4C5258A250F6F41F20833B634AB65DD37B43DA6CAD9B4D3900413F49B922467F61D874A3A56CFB9086165DBCEBDC792C42E8C40B76E9006FE373078B747146A6E12D34E86C0FEF58CE6F40C06D28E567617265EDDDF248F856E278BD3A9F7A12AF875614CBB209D6A827E485ACDB60F1A35767FFD2F1FB1FBE6AF927C59C10CBF2FE695ACD231AF64672BE65D271E4CCC2997479F806B79BE172D964A55C2B1F886D6E2490486201D97D1B99FA4D9553F3959CE0A9FC154399F7A5E02D374FC8CD779D38F9DEB26A316A0E3657B13A71908D608600243E3B57B3281AD6F9EE26882FA7E0A811F8C9FED2D1AEEAA6CCFA0EB87F8E74D82FE23DE918F0B67E3020C68BF6CCC47DDB37E374F0526CC90B952E9F47A9D40122348EFC8BC56CFAEE25361729544E938B7D67836536C9D6AAA99B63073EBF9B64E3ED6AC6B38CD8FE27DF8C70E9032774141DD3BBB497C17F6D749F55DAB9F3EC56F9534DDAE0FAF09032FF19EC89EEBCBD88B37A5A42DC6D7F5FCCBCAC5799A6EAF60B65F26DC2F20CF1304F74B9C7CDBA711F71CE374F5C071683A701C1DDC3F1C7D7CFF0178471FFE0C8FDEB71944DA8C1C5AF7E2C1E1C72166C18645ECE1FB0FBDE4AAA436DE2CC82733BABDEF48B49AD6E25381D49228BD501A43F54FEB1275FED4C62515E92D77F0A30AB5E909651663F786B2BCC3E66BCCB8D3ED16355E4E2D6C11F3151497708245D45B19FF9A361FA603A0412EEB387AF0931056B5FC3146740391FD3E0DA429EAFFDEDF40FA34B8936503DD5D82688936E2E176F0DCF22DE8D52EBCC76C1F2FAFDE9AE6F697F81CB86825FF29C2A93AE37D8EDD6FF12EFB147978FFF83573C5EDA421402FC539755D98A6E788CCD0EBE188118F4D532F40D601F043F90A841B45EFCAA8F52A441E43588928A2D96E033EC78F7E6456D432AABAA8458CC6A29268B645C56066252531D505CD233496B388D5DBFA2E6FA1FE177839ECFC5778DD26EF714E18A6582CE4CD87331D7C6ECA73FA0904BBBEB36AD51BF241A0FFDE90C3CEBF37E4C544C1CF7EEEFC31D8F6949111BC517CF98EAAB9CF71251BBB3B30D51C3BF371C6005577394DD3D8F5F35EC0FAE898A36DB6026811E718DCB028EA439D96A35A2196FB78BE43253959FC49308D1EB972F2D7C8EC6D0B16FE60C113F63A3A43B019744EDDE2B2E31AA42EF0C42642C6F2D810C47198E0DE8C0FD3A214F5513FCAC40EE147AEBF05417325B8A4E6B73270E9AA7CF82767700B233CE834B74FA70254F970666BB2D26A49F14D4F43E6129D8A25F21B75353D8ACBA3E6AC935EC26B26F2F44C9315DCA4859517F0AC68266B069BDC653EB83138263912513143773E52F3833D2634E79DEE449346CF67C7B9714F5D78130E28AE0359F14FDD381D4B3036079BC63BFD1958CF3C7C8BE3A0AE02E38D87BA667A0BE3A2E4C44AC518DDF155CD17F6107540D2680B2621B27814D6D4595AD1526D25133A741C1DD59630C95C7EF2351211158E4B559B377931EB76170EA446E16483FB54C14BE2C21B84987A8B8D404EBD494C0AA03CC59D82A0C45D6D4A00DE773D3782724E73054189576D1482B2169B80A0AC49DE1C418B530AD3F6E78E2CE6464FF6AC64FC695D6BAE09B8C9D86366D42CDC8F284D8652C044A4E7D93D7E085F32897F1E9593B8E853E2EDE42982C13730A39C4768E353BB3C197F90B0FD611393D5B72C7DB5EF6980A0DD92321CD66DD90096EF4F6528C41FD0989CDE0B4A40E8C70D58E48C530061D7F70D207CE7D401D61DB80194DCAB13808481CAA270E531B9B674647566015B1E696B61C99CCAC1527D4BE02BFB2E1A1553F3CA1ADFEDCD3CFB55E5E8BE220C2166BE7C0A8BEB34FCBCC0D6DDC02EEC6BD8A241D41EE6661F33556C32D668EA2FF52A1B18B1459D6597BCC59A37F93D4D3D9F541D74C388A9AB93812BC6BC7E0DA22143B31BCEDC11D7D92CE37045762F59344C932FC8D41B44D5810CFA1A6B689C37148E6422E96C14D55529D13026BE091BEF045531D2181A0335781214462A2BD3BB95CA29ADD94AB20DB2CD16B99395B8EDACC24A65657AB712E168B391249B348B6D5A2713B15BAA9E3A5B79F9A05AFD57CF56CB42298904AC960A49A5D525D86EFDE89192582221CEA6D0575A7FB7B1571F0A0B8CA5CB589BDFAB54396571021E21F714BFE7EAC1FC156EAC34730FF0D58BB5170AD1A47B1DC5B2B1CC92DECE888D58AE1FCBD8F8FFEAE49E534592EC0749B27354A9106F2AF3CB6CDCCA464CE660752B108084BF29592919ADE36017467C284F423552A95044E39461E628D4210A0DA4395B5163912BC2340E0932C7A0658868203A5C445B2DB9561276EA020D047F0ACB2923C655CB8CF6A4532CA20C78A74CD9D4D467F2A6961CE6A9B168911F1A8C0EB7A0722EDBC310390F314720B23D34040932C720F778680CC5D59EC908C76EFEDAB38EF1ACD8534F9FBC0FFBAA30F257206804C93B1113B60FD990B56F18E936D3A04514E95466AC2E73D0A654DEF050E3D4DA2F34501D6A8E4489BBD05054B03956ADD64243D5A1E64895FA0A0D54055ACC8CF92D77666294DC7BD7211061141A82049963D02A2734101D6E39DB277CB5CA408B521532244C818A20730CF25A1F8D41822C564285400833351641E618940C88D0B3CEE6B576617C2E5D062C358CD1B8A54BAE3773A5B9219A5AA1E6D188D9C390D8E782BA16D6A0A1EA508B81BA16D76046EA3A7836C4546DEB4D19C99CB2D833529F5CB91EE69ADBAE9DC5C94A35514DD426A25BA37BFB548756EDDB480DA15BB0F06DA53ADE55A394B7BD982DBCE206D8646DA6F2429BB6137F2668DF4C8D08C3F4A63E1603FC1BFC0218F5CC62A9C3BCC9CFAC78982716CB42F6757D6679C83EB25C90952FE50BCBB2F2412B3C8545E5312CE65CE1357C66EA159E5A6C25C417F2993D85F8B805B6A4CCFC338B2D8BF8CE3EB379111FDB6DF7C4B9AA0E9DCDD82739CEE967C22AEE31749BB11418C30C86FD4C78D49BD0CC6EAF0EB6C422EF3A0B60247C9644521ECAB521527173A51B911418EAF186797D981D6EB4EF3CAB3199778299215DF74EB41ACF8EAE839242388EE3A354B957C772DCF1DB8A1C85357FF644381B2BA260E9C3C28C683A7F4D3318EEE308FB9B9F8375E0433C7897112E41E43FC0342BDE835F1CBE3B38E43E9F329F4F992CD3D40B244789941C8178A836F627467C6CDD46E10ADBEF13305F15899E41E23E81E40F2178F9238DD5EACB217981853BA11791075F4E16FFCE531D3B17FFBCAB12EE39B97BE3D879E7FCDAED8B2379D69DBF37E2A1FFB366C95CEB2F7CB4A0519F5FDC188448E247369464B2FE70469BD6643E9BD189D75BFA6B09A69CCE1359F0B9DD772A5A10497AC065F9C9884108447D25C2A2B92CBFB2D0C2607D7DF76010A3F19F3AE84474E17306DD7A30FF89826E68DC6707BA81D19F12E886C47C1EA01B9428F9DFB192AC8C7FC7C2D1D2FCDDA018C5CB6E508C84BE37B8847EDFEB01F559D37442F1830C53B2A1D0601225C93A2D0B275E95F24AF46D9632820E7D47A6B7D3506FC1CF6E327EE5D0208A6A759018AF419B28802B8DFFCB83F79C8B1435C3CF3BF4E0164DA59809BC4868BF36D7887C5705FDF54DA8679B9B1CF5BA2229DBE53A363FABA96D559A226987D2B456DA7EBBBD4D3ABDEB7A4B7BFDEA7BDF7E30956957775A354BF5A93B214A34A8FBC2EBC5842A8DE936584A7D69D95ACBA4B272BDE93645536A4DB79AC339A569F361A84C39E13C24391F1A63481A6CC73C9FB94910F4EDD4D145D1DE213C2926C2BCDD96286F4CF0B6B7A9F346D4B3ED0D7B4ADE0F2362CB7842271197155E956FA79EDB4AC344ED0756505EEAF8755AA8D2FE3624682D5A6C20CE14A22682CAC9206CB169BAFEB465DF8468A295982C2D733E8EB4AB5C6AA0836A684B7D4EF95B40C6C72556DC69B8C26F95E9DCA460A71F77A6E4D1F8E3903597A61E8F5A8AB8CE45B7B5F65B8EAFEBD6F0A285C9FED48A5B062F0E983A554722573761D61948094A24BCF4025F6308088ECD35D59D5F0B67FFFC4556674636E2E9D0CBBEFD16C9A6BA173C73B25909A6CE8C6B53CD9F1333CD780A9D5CFE54540EE29B955CE2A62F0FAB644D8B9BD5270BEF3E468D5E38D1A4E2842AC5539DE0A90C5CA945C7E3B38E342113F6B12CA71BAD34249F1DD94C0BF9907059060ABD4111D9424B559E8F85DA2ABBFA1672631FCB722B3E432757EB5265560F0DCA0CEB28EA4CD532617CC6C23029E42BC4D0676B5757B2BCD35696C4D167AB10D7D3E54D667B6DDE248E3E6F8564DD7872B1B21E2E0AEA8803ADC49F412556C92D4FA6056B5148898B964A2415A29E89DC2B3578520A3CBAEAC986C546F9D3F988B9B66DD5B1AA3DB050AB54E65126BADDB0CE54BCCB2795EF9EB3302B539306FDF0269F82C22672D1F039EBB0F6621466BE53BCF4D9BF5186925DEDC5247D761D0B9955F1FD4DB4B7D945F8A254F1EB0CA6FE630DB142981174995D4D15E7227A88CBCD1557A2320A7789E0120D8D1EDAF29C2699FF00DC0C3DC677A4F22FDFE6F74EF04DBD7BE85D44D7BB6CBBCB509561781F307732F0264D977FAE25CB967975BDC5BFD23EAA808AE9E3BB65D7D18F3B3FF0AA729F4BAE2D2820F0EE8FDC48C26D99E19B498FAF15D2952015A60222E6AB36ADB730DC06082CBD8E36E019B6291BA2DF67F808DCD7FA928A0AA4B92158B3AFCE7CF09880302518757AF41371D80B5FBEFF3F97DABB9BE3A10000 , N'6.2.0-61023')
END

