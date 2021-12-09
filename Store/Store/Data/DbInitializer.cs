using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.Entities;
using Store.Tools;

namespace Store.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            // создать БД, если она еще не создана
            _context.Database.EnsureCreated();
            // проверка наличия ролей
            if (!_context.Roles.Any())
            {
                var roleAdmin = new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                };

                var roleUser = new IdentityRole
                {
                    Name = "user",
                    NormalizedName = "user"
                };
                // создать роль admin

                await _roleManager.CreateAsync(roleAdmin);
                await _roleManager.CreateAsync(roleUser);
                await _context.SaveChangesAsync();
            }



            // проверка наличия пользователей
            if (!_context.Users.Any())
            {
                // создать пользователя user@mail.ru
                var user = new User
                {
                    UserName = "user@mail.ru",
                    Email = "user@mail.ru",
                };
                var admin = new User
                {
                    UserName = "admin@mail.ru",
                    Email = "admin@mail.ru",
                };

                user.EmailConfirmed = true;
                admin.EmailConfirmed = true;

                await _userManager.CreateAsync(user, "123");
                await _userManager.AddToRoleAsync(user, "user");
                await _userManager.CreateAsync(admin, "123");
                await _userManager.AddToRoleAsync(admin, "admin");

                await _context.SaveChangesAsync();
            }

            if (!_context.Categories.Any())
            {
                Image image = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\Все_свечи.jpg") };
                Image insideImage = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\все_свечи_широкая.jpg") };
                _context.Images.Add(image);
                _context.Images.Add(insideImage);
                _context.SaveChanges();

                Category rootCategory = new Category
                {
                    Name = "Свечи обычные",
                    Description = "Свечи обычные описание",
                    ImageID = image.ID,
                    InsideImageID = insideImage.ID,
                    ParentID = null
                };

                _context.Categories.Add(rootCategory);
                await _context.SaveChangesAsync();


                image = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\slider\ordinary.png") };
                insideImage = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\category\ordinary.png") };
                _context.Images.Add(image);
                _context.Images.Add(insideImage);
                _context.SaveChanges();

                Category ordinary = new Category
                {
                    Name = "Обычные свечи",
                    Description = "Свечи применяются как источник освещения начиная с III тысячелетия до н. э. До появления и начала распространения электрических ламп накаливания с 1880-х годов, наряду с лампадами это был основной источник освещения. Свечи используются в этом качестве и на начало XXI века при отсутствии электричества.",
                    ImageID = image.ID,
                    InsideImageID = insideImage.ID,
                    ParentID = rootCategory.ID
                };

                _context.Categories.Add(ordinary);
                await _context.SaveChangesAsync();


                image = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\Все_свечи.jpg") };
                insideImage = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\category\aroma.png") };
                _context.Images.Add(image);
                _context.Images.Add(insideImage);
                _context.SaveChanges();
                
                Category aroma = new Category
                {
                    Name = "Ароматические свечи",
                    Description = "Ароматические свечи созданы для того, чтобы с помощью обоняния влиять на наш разум, настроение и самочувствие. Некоторые образцы помогают взбодриться, наполниться энергией и поймать волну активности. Другие имеют прямо противоположный эффект: убирают тревогу, успокаивают, снимают напряжение и помогают крепче спать.",
                    ImageID = image.ID,
                    InsideImageID = insideImage.ID,
                    ParentID = rootCategory.ID
                };
                
                _context.Categories.Add(aroma);
                await _context.SaveChangesAsync();


                image = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\Все_свечи.jpg") };
                insideImage = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\category\decorative.png") };
                _context.Images.Add(image);
                _context.Images.Add(insideImage);
                _context.SaveChanges();

                Category decorative = new Category
                {
                    Name = "Декоративные свечи",
                    Description = "Мы часто видим свечи в интерьерах частной или жилой недвижимости, но не всегда задумываемся, какие лучше использовать не только для того, чтобы время от времени смотреть на живой огонь, но и украсить пространство наилучшим образом.",
                    ImageID = image.ID,
                    InsideImageID = insideImage.ID,
                    ParentID = rootCategory.ID
                };
                
                _context.Categories.Add(decorative);
                await _context.SaveChangesAsync();



                //aroma sub categories
                Category aromaSweet = new Category
                {
                    Name = "Сладкий аромат",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = aroma.ID
                };

                Category aromaFlower = new Category
                {
                    Name = "Цветочный аромат",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = aroma.ID
                };

                Category aromaSpicy = new Category
                {
                    Name = "Пряный аромат",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = aroma.ID
                };

                Category aromaConiferous = new Category
                {
                    Name = "Хвойный аромат",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = aroma.ID
                };

                //ordinary sub categories
                Category ordinaryBig = new Category
                {
                    Name = "Большие свечи",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = ordinary.ID
                };

                Category ordinaryCompact = new Category
                {
                    Name = "Компактные свечи",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = ordinary.ID
                };

                Category ordinaryMiddle = new Category
                {
                    Name = "Средние свечи",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = ordinary.ID
                };

                Category ordinarySet = new Category
                {
                    Name = "Набор свечей",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = ordinary.ID
                };

                //decorative sub categories
                Category decorativeChristmas = new Category
                {
                    Name = "Новогодние",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = decorative.ID
                };

                //aroma sub categories
                _context.Categories.Add(aromaSweet);
                _context.Categories.Add(aromaFlower);
                _context.Categories.Add(aromaSpicy);
                _context.Categories.Add(aromaConiferous);

                //ordinary sub categories
                _context.Categories.Add(ordinaryBig);
                _context.Categories.Add(ordinaryCompact);
                _context.Categories.Add(ordinaryMiddle);
                _context.Categories.Add(ordinarySet);

                //decorative sub categories
                _context.Categories.Add(decorativeChristmas);

                await _context.SaveChangesAsync();


                //add images of candles
                Image aromaSweet1 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\sweet\almond-flower-3.png") };
                Image aromaSweet2 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\sweet\BOURBON VANILLE-21-2.png") };
                Image aromaSweet3 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\sweet\Collines_candle_citrus-infusion.png") };
                Image aromaSweet4 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\sweet\UA_260_LemonCurd.png") };
                Image aromaflower1 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\flower\Collines_bougie_250_camelia.png") };
                Image aromaflower2 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\flower\culti_candle_gelsomino_270.png") };
                Image aromaflower3 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\flower\Red-Poppies-1.png") };
                Image aromaflower4 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\flower\skandinavisk_candle_200_lempi.png") };
                Image aromaspicy1 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\spicy\ABSINTHE-2.png") };
                Image aromaspicy2 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\spicy\collines_duos-parfumes_candle_ambre-heliotrope.png") };
                Image aromaspicy3 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\spicy\Paddywax_hygge_candle_teakwoodjasmine_141.png") };
                Image aromaspicy4 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\spicy\BLOND-TABAC-Voluspa.png") };
                Image aromaconiferous1 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\сoniferous\Paddywax_glow_small_Balsam-Eucalyptus.png") };
                Image aromaconiferous2 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\сoniferous\voluspa_japonica_small_white-cypress.png") };
                Image aromaconiferous3 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\сoniferous\Paddywax_CYPRESSFIR_Small-White-Tree-Stack.png") };
                Image aromaconiferous4 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\aroma\сoniferous\Paddywax_CYPRESSFIR_Boxed-Green-Glass.png") };

                Image ordinarybig1 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\big\bolshaya-v-stekle-baltic1-11.png") };
                Image ordinarybig2 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\big\MIPABXLINSIDI_1.png") };
                Image ordinarybig3 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\big\Voluspa_AppleBlueClover_candle_25.png") };
                Image ordinarybig4 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\big\Voluspa_burning-woods_candle_850.png") };
                Image ordinarycompact1 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\compact\FM_wood-catherine.png") };
                Image ordinarycompact2 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\compact\PE0402_RGB.png") };
                Image ordinarycompact3 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\compact\Voluspa_forbidden-fig_candle_90.png") };
                Image ordinarycompact4 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\compact\Voluspa_MaisonNoir_AmberLumiere_candle_127-2.png") };
                Image ordinarymiddle1 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\middle\Collines_candle_fresh-bergamot.png") };
                Image ordinarymiddle2 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\middle\fresh-fig-3.png") };
                Image ordinarymiddle3 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\middle\GOJI-TAROCCO-ORANGE-Voluspa.png") };
                Image ordinarymiddle4 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\middle\Voluspa_japonica_candle_255_goji-tarocco-orange-1.png") };
                Image ordinaryset1 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\set\JAPONICA ARCHIVE-7291-2.png") };
                Image ordinaryset2 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\set\NATUR-Skandinavisk.png") };
                Image ordinaryset3 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\set\VANILLA-OAKMOSS-POMEGRANATE.png") };
                Image ordinaryset4 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\ordinary\set\Voluspa_candle_set2.png") };

                Image decorativechristmas1 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\decorative\christmas\christmas-balls.png") };
                Image decorativechristmas2 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\decorative\christmas\Christmas-bright-tree.png") };
                Image decorativechristmas3 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\decorative\christmas\Christmas-cone.png") };
                Image decorativechristmas4 = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\decorative\christmas\golden-tiger.png") };

                //aroma
                _context.Images.Add(aromaSweet1);
                _context.Images.Add(aromaSweet2);
                _context.Images.Add(aromaSweet3);
                _context.Images.Add(aromaSweet4);
                _context.Images.Add(aromaflower1);
                _context.Images.Add(aromaflower1);
                _context.Images.Add(aromaflower1);
                _context.Images.Add(aromaflower1);
                _context.Images.Add(aromaspicy1);
                _context.Images.Add(aromaspicy1);
                _context.Images.Add(aromaspicy1);
                _context.Images.Add(aromaspicy1);
                _context.Images.Add(aromaconiferous1);
                _context.Images.Add(aromaconiferous1);
                _context.Images.Add(aromaconiferous1);
                _context.Images.Add(aromaconiferous1);

                //ordinary
                _context.Images.Add(ordinarybig1);
                _context.Images.Add(ordinarybig2);
                _context.Images.Add(ordinarybig3);
                _context.Images.Add(ordinarybig4);
                _context.Images.Add(ordinarycompact1);
                _context.Images.Add(ordinarycompact2);
                _context.Images.Add(ordinarycompact3);
                _context.Images.Add(ordinarycompact4);
                _context.Images.Add(ordinarymiddle1);
                _context.Images.Add(ordinarymiddle2);
                _context.Images.Add(ordinarymiddle3);
                _context.Images.Add(ordinarymiddle4);
                _context.Images.Add(ordinaryset1);
                _context.Images.Add(ordinaryset2);
                _context.Images.Add(ordinaryset3);
                _context.Images.Add(ordinaryset4);

                //decorative
                _context.Images.Add(decorativechristmas1);
                _context.Images.Add(decorativechristmas2);
                _context.Images.Add(decorativechristmas3);
                _context.Images.Add(decorativechristmas4);

                _context.SaveChanges();


                //add candles
                _context.Items.AddRange(
                    new List<Item>
                    {
                        //Aroma -> sweet
                        new Item
                        {
                            Name = "ALMOND BLOSSOM, Collines de Provence",
                            Description = "Сладостный, чуть терпкий аромат ALMOND BLOSSOM – это ноты цветов миндального дерева, персика и сандала. Ваниль и сладкий миндаль делают запах гурманским. Зажгите свечу Collines de Provence и насладитесь атмосферой дома.",
                            ImageID = aromaSweet1.ID,
                            CategoryID = aromaSweet.ID,
                            Price = 3.19m
                        },
                        new Item
                        {
                            Name = "BOURBON VANILLE, Voluspa",
                            Description = "Любителям ванили посвящается! Свеча BOURBON VANILLE – это восхитительный теплый аромат дубовой бочки, в которой выдерживали кукурузный виски, и ваниль. Аромат бурбона- это аромат праздника по-американски. Ведь бурбон – это классический американский виски. Темно-золотистый цвет напитка насыщен ароматом сухофруктов, меда, и хранит в себе ноты древесины дуба.\n\nВсе эти запахи безукоризненно воплощены в аромате BOURBON VANILLE. Идеальный аккомпанемент – стручок французской ванили, который добавляет сладости и легкости этому легкому и праздничному аромату от Voluspa. А роскошная упаковка делает эту свечу просто совершенным подарком.",
                            ImageID = aromaSweet2.ID,
                            CategoryID = aromaSweet.ID,
                            Price = 4.29m
                        },
                        new Item
                        {
                            Name = "CITRUS INFUSION, Collines de Provence",
                            Description = "Аромасвеча CITRUS INFUSION – это абсолютно гурманский аромат. Микс апельсина, абрикосов, ванили, коричневого сахара и сочной красной смородины. Нотки какао в финале добавляют аромату мягкости.\n\nСвеча Collines de Provence выполнена из матового стекла и имеет керамическую крышечку, которая защитит поверхность свечи от пыли.",
                            ImageID = aromaSweet3.ID,
                            CategoryID = aromaSweet.ID,
                            Price = 2.09m
                        },
                        new Item
                        {
                            Name = "LEMON CURD, Urban Apothecary",
                            Description = "Английский десерт лимонный курд – это заварной крем с ярким цитрусовым вкусом. Аромат свечи LEMON CURD от Urban Apothecary посвящен именно ему. Сладко-цитрусовый, в нем хорошо ощущается цедра лимона, тягучая сладость взбитых сливок с капелькой апельсинового ликера, а ванильно-кокосовое звучание добавляет запаху законченное звучание. Очень вкусный аромат, который мгновенно настроит на ужин или субботний завтрак.\n\nСредняя свеча в прозрачной баночке с металлическим замочком так и намекает: на кухне мне самое место!",
                            ImageID = aromaSweet4.ID,
                            CategoryID = aromaSweet.ID,
                            Price = 4.99m
                        },

                        //Aroma -> flower
                        new Item
                        {
                            Name = "CAMELIA, Collines de Provence",
                            Description = "Тонкий и элегантный аромат свечи CAMELIA от Collines de Provence понравится поклонникам запахов белых цветов. Выразительный, и при этом деликатный, это запах с нотами садовых роз, белоснежных гвоздик и удивительный аромат камелии. Кстати, цветы камелии не имеют запаха. Но парфюмеры решили исправить это недоразумение и синтетически создали аромат этому цветку. Традиционно он звучит как смесь чайных нот и пиона.\n\nСвеча в стеклянном стакане белого цвета CAMELIA станет идеальным подарком: она упакована в очаровательную коробочку с лентой. Изящно и со вкусом!",
                            ImageID = aromaflower1.ID,
                            CategoryID = aromaFlower.ID,
                            Price = 3.7m
                        },
                        new Item
                        {
                            Name = "GELSOMINO, Culti Milano",
                            Description = "Джельсомино – это жасмин по-итальянски. И в аромате этой свечи Culti Milano солирует именно он. А благодаря мягкому запаху цветов апельсина, весь аромат становится очень летним, солнечным и очень теплым. Свеча GELSOMINO точно понравится и поклонникам цветочных ароматов, и тем, кому близки ароматы липы и нероли.\n\nКаждая свеча Culti Milano упакована в подарочную коробочку, и она точно станет приятным презентом к любому празднику.",
                            ImageID = aromaflower2.ID,
                            CategoryID = aromaFlower.ID,
                            Price = 3.69m
                        },
                        new Item
                        {
                            Name = "RED POPPIES, BROAD st. BRAND",
                            Description = "Парфюмеры BROAD st. BRAND заботятся о том, чтобы жители мегаполисов могли отдохнуть от каменных джунглей и почувствовать свою близость к природе. Именно поэтому они создали аромат RED POPPIES. Его основная нота – красный мак. Изящное растение, с невероятно красивыми цветками, которые пленяют нежнейшим запахом.\nЗажгите свечу RED POPPIES, и ваш дом наполнится ароматом цветов: жизнерадостным, стремительным и сладостным. Этот аромат и правда – сладостный! Девушки его просто обожают, и возвращаются за ним снова и снова.",
                            ImageID = aromaflower3.ID,
                            CategoryID = aromaFlower.ID,
                            Price = 4.19m
                        },
                        new Item
                        {
                            Name = "LEMPI, Skandinavisk",
                            Description = "Чем пахнет скандинавская любовь? Конечно, цветами. Нежные лепестки роз, садовые пионы и свежая нота мха – основа аромата LEMPI от Skandinavisk. Аромат спелой клубники делает запах еще более романтичным и манящим. Зажгите эту свечу, и пусть аромат любви наполнит весь ваш дом!\n\nСвеча в стеклянном стаканчике с крышкой из бука упакована в стильную коробочку. И она станет идеальным подарком поклоннику скандинавского дизайна.",
                            ImageID = aromaflower4.ID,
                            CategoryID = aromaFlower.ID,
                            Price = 4.89m
                        },

                        //Aroma -> spicy
                        new Item
                        {
                            Name = "ABSINTHE, BROAD st. BRAND",
                            Description = "Культовый напиток богемы, “зеленая фея” – спутница художников Мане и Ван Гога. Это аромат тонкий и с выраженными нотами полыни и мяты. Лучшее начало пятничного вечера на Broad Street – заглянуть в один из баров и заказать порцию абсента.\n\nИли зажечь свечу ABSINTHE и насладиться расслабляющим ароматом. Обещаем, это будет лучшее начало уикенда!\n\nА еще это аромат, который можно смело дарить мужчине: он оценит ваш тонкий вкус и будет с удовольствием зажигать его дома. В нем нет алкогольных нот и звучать он будет, скорее, фоном.",
                            ImageID = aromaspicy1.ID,
                            CategoryID = aromaSpicy.ID,
                            Price = 2.99m
                        },
                        new Item
                        {
                            Name = "AMBER & HELIOTROPE, Collines de Provence",
                            Description = "Французский бренд Collines de Provence не перестает радовать нас новыми ароматами! В новой элегантной упаковке из матового стекла! Аромат AMBER & HELIOTROPE удивительный! Многогранное, сложное сочетание нот амбры, гелиотропа, сандала и герани.\n\nВоск окрашен пищевым красителем в терракотовый цвет, напоминающий красный восточный песок. Этот аромат унесет вас в самое сердце Востока! Самое время зажечь свечу и насладится прекрасным ароматом роскошной свечи.",
                            ImageID = aromaspicy2.ID,
                            CategoryID = aromaSpicy.ID,
                            Price = 3.99m
                        },
                        new Item
                        {
                            Name = "TEAKWOOD + JASMINE, PaddyWax",
                            Description = "Еще одна прекрасная новинка от PaddyWax! Коллекция Hygge включает теплые домашние ароматы. Когда хочется свернутся калачиком под мягким теплым пледом и читать любимую книгу. Эти свечи точно будут кстати! Аромат TEAKWOOD + JASMINE яркий и в то же время нежный и ненавязчивый. В составе – тиковое дерево, бергамот и теплая амбра. Легкие ноты жасмина отлично вписываются в общую композицию.\n\nСвеча в керамической баночке небесно-голубого цвета выглядит роскошно и передаёт настроение аромата. Однозначно советуем попробовать эту новинку! Сейчас самое время!",
                            ImageID = aromaspicy3.ID,
                            CategoryID = aromaSpicy.ID,
                            Price = 4.59m
                        },
                        new Item
                        {
                            Name = "BLOND TABAC, Voluspa",
                            Description = "Драгоценные украшения – вдохновение для создания свечи Blond Tabac от Voluspa. Элегантная упаковка в матовом золоте и изысканное сочетание ароматов.\nТеплый, слегка землистый аромат листьев табака Перик с пряными нюансами, стручковая ваниль и сандаловое дерево создают благородное звучание. Драгоценным является и сам табак Перик, который растет только в северной Америке на небольшой территории Сент-Джеймс.\nЗажгите аромат свечи Blond Tabac и погрузитесь в атмосферу 19 века: с пышными платьями, роскошными интерьерами и красивыми ритуалами.",
                            ImageID = aromaspicy4.ID,
                            CategoryID = aromaSpicy.ID,
                            Price = 4.99m
                        },


                        //Aroma -> coniferous
                        new Item
                        {
                            Name = "BALSAM & EUCALYPTUS, PaddyWax",
                            Description = "Если вы поклонник хвойных ароматов, то не пропустите эту свечу BALSAM & EUCALYPTUS от PaddyWax. Хвойно-пряный, с травяными нотами, этот запах звучит насыщенно и интересно. Запах еловой хвои, сосновые шишки смягчены теплой замшей и пачули. А травяное звучание аромат приобрел благодаря сочной мяте и эвкалипту.\n\nИ только посмотрите, какая красивая свеча получилась: глазурованная керамика с сияющими разводами, золотистая крышка из металла: глаз не оторвать!",
                            ImageID = aromaconiferous1.ID,
                            CategoryID = aromaConiferous.ID,
                            Price = 2.89m
                        },
                        new Item
                        {
                            Name = "WHITE CYPRESS Средняя свеча в стеклянной баночке, Voluspa",
                            Description = "Еще один лимитированный аромат для дома от бренда Voluspa. Теплый, уютный запах WHITE CYPRESS – это ноты кипариса. Немного хвои, немного древесины и очень много удовольствия и ощущения дома. А еще этот аромат Voluspa моментально создаст дома ощущение живой ели: отличный вариант, если вы не планируете покупку живого дерева к празднику. У свечи “Белый кипарис” очень красивая упаковка из сине-зеленого стекла, как будто хвойный лес припорошен снегом. И только взгляните на эту стеклянную баночку свечи, ее из рук выпускать не хочется.",
                            ImageID = aromaconiferous2.ID,
                            CategoryID = aromaConiferous.ID,
                            Price = 2.99m
                        },
                        new Item
                        {
                            Name = "CYPRESS & FIR MINI, PaddyWax",
                            Description = "Свеча-елочка от PaddyWax – это абсолютный восторг! Правда!\n\nКерамическая елочка из двух частей: нижняя – это свеча с ароматом хвои ели, кипариса, сосновых шишечек и эвкалипта. А верхняя – может служить декором, а может стать подставкой для благовоний. Составьте части и перед вами – очаровательная белая елочка, как будто припорошенная снежком. Отличное украшение праздничного стола и декор для дома. А еще вы можете собрать целую еловую полянку из свечей-елочек разного размера и цвета. Здорово, правда?",
                            ImageID = aromaconiferous3.ID,
                            CategoryID = aromaConiferous.ID,
                            Price = 2.99m
                        },
                        new Item
                        {
                            Name = "CYPRESS & FIR, PaddyWax",
                            Description = "Свеча в стаканчике хвойного цвета с золотистыми узорами CYPRESS & FIR упакована в красивую праздничную коробочку. Лаконичное и элегантное решение для подарка от PaddyWax! Внутри – самый новогодний аромат: Ноты хвои ели, кипариса, сосновых шишечек и эвкалипта мгновенно наполнит дом ощущением праздника, как бы погода не была за окном.",
                            ImageID = aromaconiferous4.ID,
                            CategoryID = aromaConiferous.ID,
                            Price = 3.99m
                        },

                        //ordinary -> big
                        new Item
                        {
                            Name = "BALTIC AMBER, Voluspa",
                            Description = "Волнующий теплый аромат смолы янтарного дерева, ноты сандала, кедра и послевкусие цветка ванили.. BALTIC AMBER – это один из самых благородных запахов коллекции.  Нежный, сладковатый и чуть пряный. Бархатный аромат этой свечи Voluspa подойдет для вечера: уютного и спокойного.",
                            ImageID = ordinarybig1.ID,
                            CategoryID = ordinaryBig.ID,
                            Price = 2.49m
                        },
                        new Item
                        {
                            Name = "INSIDIAE, Apotheca",
                            Description = "Аромат соснового леса ночью: мистический, окутывающий и внушающий благоговение перед силами природы. Свеча INSIDIAE – это хвоя и древесина кедра, много воздуха и свежести.\n\nЕсли вы уже влюбились в этот аромат Apotheca, закажите большую свечу: она будет радовать вас целых 200 часов. Такая свеча отлично подойдет для большой квартиры или загородного дома. У нее 5 фитилей, которые обеспечивают очень ровное плавление воска и насыщенный аромат.",
                            ImageID = ordinarybig2.ID,
                            CategoryID = ordinaryBig.ID,
                            Price = 3.49m
                        },
                        new Item
                        {
                            Name = "APPLE BLUE CLOVER, Voluspa",
                            Description = "Свежий, хрустящий аромат с тонкой фруктовой нотой яблока. Свеча APPLE BLUE CLOVER от Voluspa понравится и поклонником запахов ягод и фруктов, и тем, кто любит освежающие ароматы. Эта свеча как будто бы пахнет горным воздухом, весенней зеленью и свободой. Ноты клевера, мха и огурца делают аромат волнующим и легким.\n\nСвеча большого объема Voluspa подойдет для загородного дома или просторной квартиры. В ней почти 3,5 кг воска, так что гореть она будет целых 250 часов. А еще у этой свечи Voluspa 5 фитилей и потрясающе красивая коробка.",
                            ImageID = ordinarybig3.ID,
                            CategoryID = ordinaryBig.ID,
                            Price = 4.49m
                        },
                        new Item
                        {
                            Name = "BURNING WOODS, Voluspa",
                            Description = "Поклонники дымных ароматов точно оценят новинку от Voluspa. Аромат BURNING WOODS напомнит вам теплые летние вечера у костра и скрасит любую осеннюю непогоду! В составе – кедр, можжевельник и тлеющее дерево. Интересное сочетание, правда?\n\nСвеча в большом стакане из фактурного белого стекла с крышкой выглядит потрясающе! Она идеально впишется в просторной гостиной или спальне. Объем воска рассчитан на 80 часов горения. Эта свеча будет радовать вас очень долго!",
                            ImageID = ordinarybig4.ID,
                            CategoryID = ordinaryBig.ID,
                            Price = 2.39m
                        },

                        //ordinary -> compact
                        new Item
                        {
                            Name = "CATHERINE, Flame Moscow",
                            Description = "Аромат CATHERINE, пожалуй, самый уютный из новинок Flame Moscow! Ноты какао, кардамона, корицы и шоколада так и манят! А шалфей, папирус и красное дерево прекрасно оттеняют аромат, добавляя свежую ноту.\n\nКомпактная свеча из стекла серого цвета имеет деревянный фитиль, который будет уютно потрескивать при горении. Эта свеча отлично подойдет в качестве небольшого презента коллеге или подружке!",
                            ImageID = ordinarycompact1.ID,
                            CategoryID = ordinaryCompact.ID,
                            Price = 2.49m
                        },
                        new Item
                        {
                            Name = "FRANCESCO, PaddyWax",
                            Description = "Знакомьтесь – Франческо! Так зовут эту свечу-персону. Добродушный Франческо встречает закат, лежа в гамаке и попыхивая сигарой. Он умеет наслаждаться жизнью как никто другой. Неспешно и со вкусом.\n\nАромат свечи FRANCESCO – это томный запах дыма и сладостной амбры, мягкий и уютный. Он напомнит вам вечер у камина или костра. У каждой свечи Persona от PaddyWax – свой характер. А еще очаровательная шляпка, которую можно использовать как крышку и подставку под спички или палочки благовоний.",
                            ImageID = ordinarycompact2.ID,
                            CategoryID = ordinaryCompact.ID,
                            Price = 2.99m
                        },
                        new Item
                        {
                            Name = "FORBIDDEN FIG, Voluspa",
                            Description = "Свеча FORBIDDEN FIG от Voluspa – это аромат-осень! Инжир просто повсюду! В составе – плоды и листья инжира. Роза прекрасно и ненавязчиво дополняет аромат.\n\nСвеча небольшого размера в баночке резного стекла с металлической крышкой: это всегда отличная идея! Отлично подойдет в качестве небольшого подарка маме или любимой подруге. Всем советуем попробовать новинку американского бренда!",                            
                            ImageID = ordinarycompact3.ID,
                            CategoryID = ordinaryCompact.ID,
                            Price = 3.99m
                        },
                        new Item
                        {
                            Name = "AMBRE LUMIERE NEW, Voluspa",
                            Description = "Древесный аромат с нотами благовоний и сладостной амбры – это AMBRE LUMIERE от Voluspa. Теплый, мягкий, очень уютный и совершенно универсальный, не смотря на яркие компоненты в составе.\n\nА свеча небольшого размера в баночке резного стекла с металлической крышкой: это всегда отличная идея! С нее можно начать знакомство с одним из самых популярных ароматов калифорнийского бренда.",
                            ImageID = ordinarycompact4.ID,
                            CategoryID = ordinaryCompact.ID,
                            Price = 2.99m
                        },
                        
                        //ordinary -> middle
                        new Item
                        {
                            Name = "FRESH BERGAMOT, Collines de Provence",
                            Description = "Аромат свечи FRESH BERGAMOT создан для поклонников травяных и цитрусовых ароматов. Сочный базилик, горьковатый бергамот и лимон. Теплая база из сандала и кедра делает аромат более стойким. А нероли добавляет сладости. Это очень красивый аромат, который максимально ассоциируется с летом в Средиземноморье.\n\nСвеча Collines de Provence выполнена из матового стекла и имеет керамическую крышечку, которая защитит поверхность свечи от пыли.",
                            ImageID = ordinarymiddle1.ID,
                            CategoryID = ordinaryMiddle.ID,
                            Price = 4.99m
                        },
                        new Item
                        {
                            Name = "FRESH FIG Средняя свеча в стекле, Collines de Provence",
                            Description = "Всегда уместный, спокойный и взвешенный аромат, с тонкой пудровой ноткой. Сомневаетесь в выборе, выбирайте инжир. Аромат свечи FRESH FIG – не исключение. Спелые плоды фигового дерева и немного зелени: идеально! Зажгите свечу Collines de Provence и насладитесь атмосферой дома.",
                            ImageID = ordinarymiddle2.ID,
                            CategoryID = ordinaryMiddle.ID,
                            Price = 4.99m
                        },
                        new Item
                        {
                            Name = "GOJI TAROCCO ORANGE, Voluspa",
                            Description = "Свеча GOJI TAROCCO ORANGE – один из лучших запахов от Voluspa: терпкая ягода годжи, спелый манго и кисло-сладкий красный апельсин. Сочный коктейль, от которого у вас буквально потекут слюнки. Волнующий, насыщенный, сочный – идеален для начала дня: яркий и волнующий, он доставит вам подлинное наслаждение.\n\nЭта свеча Voluspa рассчитана на 50 часов горения. Очаровательная баночка резного стекла со стеклянной крышкой пленит вас с первого взгляда. Только представьте, как красиво будет смотреться огонь сквозь алое стекло!",
                            ImageID = ordinarymiddle3.ID,
                            CategoryID = ordinaryMiddle.ID,
                            Price = 4.99m
                        },
                        new Item
                        {
                            Name = "GOJI TAROCCO ORANGE, Voluspa",
                            Description = "Сочный, сладкий аромат! Терпкая ягода годжи, спелый манго и кисло-сладкий красный апельсин. Сочный коктейль, от которого у вас буквально потекут слюнки. Волнующий, насыщенный, сочный – идеален для начала дня: яркий и волнующий, он доставит вам подлинное наслаждение.\n\nКрасивая и элегантная свеча в стеклянном стаканчике и подарочной коробочке – готовый презент к любому торжеству. И только посмотрите на цвет стекла и представьте, как красиво его будет подсвечивать огонь: сплошное эстетическое удовольствие!",
                            ImageID = ordinarymiddle4.ID,
                            CategoryID = ordinaryMiddle.ID,
                            Price = 2.99m
                        },

                        
                        //ordinary -> set
                        new Item
                        {
                            Name = "JAPONICA ARCHIVE, Voluspa",
                            Description = "Роскошный подарочный набор из 12 мини-свечей Voluspa. В этом наборе вы найдете самые популярные ароматы коллекции Japonica: сочные, сладкие, хвойные и пряные. Все, чтобы угодить адресату подарка. И только взгляните на упаковку: великолепный кейс впечатлит самого взыскательного человека. Согласны?",
                            ImageID = ordinaryset1.ID,
                            CategoryID = ordinarySet.ID,
                            Price = 2.99m
                        },
                        new Item
                        {
                            Name = "NATUR, Skandinavisk",
                            Description = "В подарочном наборе NATUR бренд Skandinavisk собрал 3 аромата, вдохновленных северной природой. FJORD – легкий и волнующий запах в честь фьордов, OY – это свеча вдохновленная островами и HAV – очень натуралистичный аромат северного моря.\n\nЭтот презент абсолютно точно понравится поклонникам Скандинавии и дикой природы. А еще тем, кто предпочитает свежие и акватические запахи.",
                            ImageID = ordinaryset2.ID,
                            CategoryID = ordinarySet.ID,
                            Price = 3.39m
                        },
                        new Item
                        {
                            Name = "VANILLA+OAKMOSS & POMEGRANATE+SPRUCE GIFT BOX, PaddyWax",
                            Description = "Тук-тук. Это вечерняя доставка удовольствия Relish.\nВам понадобится: 1 ванная, 2 свечи и 20 минут\nРецепт 1. Ваниль и дубовый мох – сочетание неожиданное, но какое приятное! Наполните теплую ванную, добавьте пену вашу любимую, налейте бокал шампанского и зажгите свечу VANILLA + OAKMOSS. Осталось лишь насладиться моментом.\nРецепт 2. Терпко-сладкие, винные нюансы свечи POMEGRANATE & SPRUCE как будто предназначены для романтических вечеров. С приглушенным светом, лепестками роз, пеной и всем тем, от чего так сладостно кружится голова…\n\nВыбирайте аромат под настроение или сочетайте их между собой. 2 теплых и насыщенных запаха в наборе Relish – то, что нужно для приятного вечера.",
                            ImageID = ordinaryset3.ID,
                            CategoryID = ordinarySet.ID,
                            Price = 4.49m
                        },
                        new Item
                        {
                            Name = "JAPONICA BESTSELLERS PEDESTAL, Voluspa",
                            Description = "JAPONICA BESTSELLERS PEDESTAL 4 – это набор самых популярных ароматов Voluspa в этом сезоне! Сочный и сладкий GOJI TAROCCO ORANGE с нотами ягод годжи, апельсина и манго, теплый BALTIC AMBER с нотами ванили, сандала, амбры, мягкий древесный FRENCH CADE LAVENDER с нотами лаванды и можжевельника. И очень интересная новинка этой осени – FORBIDDEN FIG! Это фруктово-травяной аромат листьев и плодов инжира.\n\nСвечи упакованы в роскошный кейс – очень красивый и солидный подарок! Вы можете зажигать свечи по отдельности или составить свою особенную композицию. Очень удобно, правда? Нам очень нравится!",
                            ImageID = ordinaryset4.ID,
                            CategoryID = ordinarySet.ID,
                            Price = 4.49m
                        },


                        //decorative -> decorativeChristmas
                        new Item
                        {
                            Name = "Свеча, 6 см, в ассортименте, красная/серебристая/золотистая, Шар, Christmas",
                            Description = "Размер: 6 см.\nМатериал: парафин\nНе зажигать вблизи легковоспламеняющихся поверхностей. Хранить в местах, недоступных для детей.",
                            ImageID = decorativechristmas4.ID,
                            CategoryID = decorativeChristmas.ID,
                            Price = 2.19m
                        },
                        new Item
                        {
                            Name = "Свеча, 14 см, зеленая, Елка заснеженная, Christmas bright ",
                            Description = "Свеча.\nРазмер: 14 см.\nМатериал: парафин.\nХранить в недоступном для детей месте. Не зажигать вблизи легковоспламеняющихся предметов. Температура хранения от 0-35℃.",
                            ImageID = decorativechristmas2.ID,
                            CategoryID = decorativeChristmas.ID,
                            Price = 2.29m
                        },
                        new Item
                        {
                            Name = "Свеча, 12 см, серая, Шишка, Christmas",
                            Description = "Свеча.\nРазмер: 12 см.\nМатериал: парафин.\nХранить в недоступном для детей месте. Не зажигать вблизи легковоспламеняющихся предметов. Температура хранения от 0-35℃.",
                            ImageID = decorativechristmas1.ID,
                            CategoryID = decorativeChristmas.ID,
                            Price = 3.79m
                        },                        
                        new Item
                        {
                            Name = "Свеча, 13 см, золотистая, блестки, Тигр, Tiger",
                            Description = "Свеча.\nРазмер: 13 см.\nМатериал: парафин.\nНе зажигать вблизи легковоспламеняющихся предметов. Хранить в недоступном для детей месте. Температура хранения от 0-35℃",
                            ImageID = decorativechristmas3.ID,
                            CategoryID = decorativeChristmas.ID,
                            Price = 4.69m
                        },                       
                    }); 
                await _context.SaveChangesAsync();
            }

        }
    }
}