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


                image = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\category\ordinary.png") };
                insideImage = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\cat1.png") };
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


                image = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\category\aroma.png") };
                insideImage = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\cat2.png") };
                _context.Images.Add(image);
                _context.Images.Add(insideImage);
                _context.SaveChanges();
                
                Category aroma = new Category
                {
                    Name = "Aroma candles",
                    Description = "Scented candles are created in order to influence our mind, mood and well-being with the help of the sense of smell. Some patterns help to cheer up, fill with energy and catch a wave of activity. Others have the exact opposite effect: relieve anxiety, calm down, relieve tension, and help you sleep better.",
                    ImageID = image.ID,
                    InsideImageID = insideImage.ID,
                    ParentID = rootCategory.ID
                };
                
                _context.Categories.Add(aroma);
                await _context.SaveChangesAsync();


                image = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\category\decorative.png") };
                insideImage = new Image { ImageData = ImageConverter.ImageToBase64(Environment.CurrentDirectory + @"\Init\candles\cat3.png") };
                _context.Images.Add(image);
                _context.Images.Add(insideImage);
                _context.SaveChanges();

                Category decorative = new Category
                {
                    Name = "Decorative candles",
                    Description = "We often see candles in the interiors of private or residential real estate, but we do not always think about which ones are better to use not only in order to look at a live fire from time to time, but also to decorate the space in the best possible way.",
                    ImageID = image.ID,
                    InsideImageID = insideImage.ID,
                    ParentID = rootCategory.ID
                };
                
                _context.Categories.Add(decorative);
                await _context.SaveChangesAsync();



                //aroma sub categories
                Category aromaSweet = new Category
                {
                    Name = "Sweet scent",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = aroma.ID
                };

                Category aromaFlower = new Category
                {
                    Name = "Flower scent",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = aroma.ID
                };

                Category aromaSpicy = new Category
                {
                    Name = "Spicy flavor",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = aroma.ID
                };

                Category aromaConiferous = new Category
                {
                    Name = "Coniferous aroma",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = aroma.ID
                };

                //ordinary sub categories
                Category ordinaryBig = new Category
                {
                    Name = "Big candles",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = ordinary.ID
                };

                Category ordinaryCompact = new Category
                {
                    Name = "Compact candles",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = ordinary.ID
                };

                Category ordinaryMiddle = new Category
                {
                    Name = "Medium candles",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = ordinary.ID
                };

                Category ordinarySet = new Category
                {
                    Name = "Set of candles",
                    Description = "",
                    ImageID = 0,
                    InsideImageID = 0,
                    ParentID = ordinary.ID
                };

                //decorative sub categories
                Category decorativeChristmas = new Category
                {
                    Name = "Christmas candles",
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
                _context.Images.Add(aromaflower2);
                _context.Images.Add(aromaflower3);
                _context.Images.Add(aromaflower4);
                _context.Images.Add(aromaspicy1);
                _context.Images.Add(aromaspicy2);
                _context.Images.Add(aromaspicy3);
                _context.Images.Add(aromaspicy4);
                _context.Images.Add(aromaconiferous1);
                _context.Images.Add(aromaconiferous2);
                _context.Images.Add(aromaconiferous3);
                _context.Images.Add(aromaconiferous4);

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
                            Description = "The sweet, slightly tart aroma of ALMOND BLOSSOM contains notes of almond blossom, peach and sandalwood. Vanilla and sweet almonds add a gourmand flavor. Light the Collines de Provence candle and enjoy the atmosphere at home.",
                            ImageID = aromaSweet1.ID,
                            CategoryID = aromaSweet.ID,
                            Price = 3.19m
                        },
                        new Item
                        {
                            Name = "BOURBON VANILLE, Voluspa",
                            Description = "Dedicated to vanilla lovers! BOURBON VANILLE Candle is a delicious warm oak barrel aged corn whiskey and vanilla. The scent of bourbon is an American holiday scent. After all, bourbon is a classic American whiskey. The dark golden color of the drink is saturated with the aroma of dried fruits, honey, and retains the notes of oak wood. All these scents are impeccably embodied in the BOURBON VANILLE fragrance. The perfect accompaniment is a French vanilla pod that adds sweetness and lightness to this light and festive fragrance from Voluspa. And the luxurious packaging makes this candle a perfect gift.",
                            ImageID = aromaSweet2.ID,
                            CategoryID = aromaSweet.ID,
                            Price = 4.29m
                        },
                        new Item
                        {
                            Name = "CITRUS INFUSION, Collines de Provence",
                            Description = "The CITRUS INFUSION aroma candle is an absolutely gourmand scent. A mix of orange, apricot, vanilla, brown sugar and juicy red currant. The cocoa notes in the finish add softness to the aroma. The Collines de Provence candle is made of frosted glass and has a ceramic lid that protects the surface of the candle from dust.",
                            ImageID = aromaSweet3.ID,
                            CategoryID = aromaSweet.ID,
                            Price = 2.09m
                        },
                        new Item
                        {
                            Name = "LEMON CURD, Urban Apothecary",
                            Description = "The English Lemon Kurdish dessert is a custard with a rich citrus flavor. The aroma of the LEMON CURD candle from Urban Apothecary is dedicated to him. Sweet citrus, lemon zest is well felt in it, the viscous sweetness of whipped cream with a drop of orange liqueur, and the vanilla-coconut sound adds a complete sound to the smell. A delicious scent that will instantly set you up for dinner or Saturday breakfast. The middle candle in a transparent jar with a metal lock hints: I belong in the kitchen!",
                            ImageID = aromaSweet4.ID,
                            CategoryID = aromaSweet.ID,
                            Price = 4.99m
                        },

                        //Aroma -> flower
                        new Item
                        {
                            Name = "CAMELIA, Collines de Provence",
                            Description = "The subtle and elegant scent of the CAMELIA candle by Collines de Provence will appeal to fans of white flowers. Expressive, and at the same time delicate, it is a scent with notes of garden roses, snow-white carnations and an amazing scent of camellia. By the way, camellia flowers are odorless. But the perfumers decided to correct this misunderstanding and synthetically created a fragrance for this flower. Traditionally, it sounds like a mixture of tea notes and peony. The candle in a white glass CAMELIA is an ideal gift: it is packed in a charming box with a ribbon. Elegant and tasteful!",
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
                            Description = "The subtle and elegant scent of the CAMELIA candle by Collines de Provence will appeal to fans of white flowers. Expressive, and at the same time delicate, it is a scent with notes of garden roses, snow-white carnations and an amazing scent of camellia. By the way, camellia flowers are odorless. But the perfumers decided to correct this misunderstanding and synthetically created a fragrance for this flower. Traditionally, it sounds like a mixture of tea notes and peony. The candle in a white glass CAMELIA is an ideal gift: it is packed in a charming box with a ribbon. Elegant and tasteful!",
                            ImageID = aromaflower3.ID,
                            CategoryID = aromaFlower.ID,
                            Price = 4.19m
                        },
                        new Item
                        {
                            Name = "LEMPI, Skandinavisk",
                            Description = "What does Scandinavian love smell like? Flowers, of course. Delicate rose petals, garden peonies and a fresh note of moss are the basis of Skandinavisk's LEMPI fragrance. The aroma of ripe strawberries makes the scent even more romantic and inviting. Light this candle and let the scent of love fill your entire home! The candle in a glass cup with a beech lid is packed in a stylish box. And it will be the perfect gift for a fan of Scandinavian design.",
                            ImageID = aromaflower4.ID,
                            CategoryID = aromaFlower.ID,
                            Price = 4.89m
                        },

                        //Aroma -> spicy
                        new Item
                        {
                            Name = "ABSINTHE, BROAD st. BRAND",
                            Description = "The iconic bohemian drink, the “green fairy” is the companion of the artists Manet and Van Gogh. It is a delicate aroma with pronounced notes of wormwood and mint. The best way to start a Friday night on Broad Street is to pop into one of the bars for a serving of absinthe. Or light an ABSINTHE candle and enjoy the relaxing scent. We promise this will be the best start to the weekend! And it is also a scent that you can safely give a man: he will appreciate your delicate taste and will be happy to light it at home. There are no alcoholic notes in it and it will sound rather in the background.",
                            ImageID = aromaspicy1.ID,
                            CategoryID = aromaSpicy.ID,
                            Price = 2.99m
                        },
                        new Item
                        {
                            Name = "AMBER & HELIOTROPE, Collines de Provence",
                            Description = "The French brand Collines de Provence never ceases to delight us with new fragrances! New, elegant frosted glass packaging! AMBER & HELIOTROPE is amazing! A multifaceted, complex combination of amber, heliotrope, sandalwood and geranium notes. The wax is colored with food coloring in a terracotta color, reminiscent of the red oriental sand. This fragrance will take you to the very heart of the East! It's time to light a candle and enjoy the wonderful scent of a luxurious candle.",
                            ImageID = aromaspicy2.ID,
                            CategoryID = aromaSpicy.ID,
                            Price = 3.99m
                        },
                        new Item
                        {
                            Name = "TEAKWOOD + JASMINE, PaddyWax",
                            Description = "Another great new product from PaddyWax! The Hygge collection includes warm home fragrances. When you want to curl up under a soft warm blanket and read your favorite book. These candles will definitely come in handy! The aroma of TEAKWOOD + JASMINE is bright and at the same time delicate and unobtrusive. It contains teak wood, bergamot and warm amber. Light notes of jasmine fit perfectly into the overall composition. The candle in a ceramic jar in sky blue looks luxurious and conveys the mood of the fragrance. We definitely recommend you try this new product! Right now!",
                            ImageID = aromaspicy3.ID,
                            CategoryID = aromaSpicy.ID,
                            Price = 4.59m
                        },
                        new Item
                        {
                            Name = "BLOND TABAC, Voluspa",
                            Description = "The precious jewelry is the inspiration for the Blond Tabac candle from Voluspa. Elegant packaging in matt gold and a sophisticated combination of aromas. Warm, slightly earthy scent of tobacco leaves Perique with spicy nuances, vanilla pods and sandalwood create a noble sound. The Perique tobacco itself, which grows only in North America in the small area of St. James, is also precious. Light the scent of a Blond Tabac candle and immerse yourself in the atmosphere of the 19th century: with lush dresses, luxurious interiors and beautiful rituals.",
                            ImageID = aromaspicy4.ID,
                            CategoryID = aromaSpicy.ID,
                            Price = 4.99m
                        },


                        //Aroma -> coniferous
                        new Item
                        {
                            Name = "BALSAM & EUCALYPTUS, PaddyWax",
                            Description = "If you are a fan of coniferous scents, then don't miss this BALSAM & EUCALYPTUS candle by PaddyWax. Spicy coniferous, with herbal notes, this smell sounds rich and interesting. The scent of spruce needles, pine cones are softened by warm suede and patchouli. And the aroma acquired a herbal sound thanks to juicy mint and eucalyptus.\n\nAnd just look what a beautiful candle it turned out: glazed ceramics with shining stains, golden metal lid: you can't take your eyes off!",
                            ImageID = aromaconiferous1.ID,
                            CategoryID = aromaConiferous.ID,
                            Price = 2.89m
                        },
                        new Item
                        {
                            Name = "WHITE CYPRESS Средняя свеча в стеклянной баночке, Voluspa",
                            Description = "Another limited edition home fragrance from the Voluspa brand. The warm, cozy scent of WHITE CYPRESS is cypress. A little pine needles, a little wood, and a lot of fun and home feeling. And this Voluspa scent will instantly create the feeling of living spruce at home: a great option if you are not planning to buy a living tree for the holiday. The White Cypress candle has a very beautiful package made of blue-green glass, as if a coniferous forest is covered with snow. And just look at this glass candle jar, you don't want to let go of it.",
                            ImageID = aromaconiferous2.ID,
                            CategoryID = aromaConiferous.ID,
                            Price = 2.99m
                        },
                        new Item
                        {
                            Name = "CYPRESS & FIR MINI, PaddyWax",
                            Description = "The PaddyWax herringbone candle is an absolute delight! True!\n\nCeramic herringbone in two parts: the lower one is a candle with the scent of spruce needles, cypress, pine cones and eucalyptus. And the top one can serve as a decor, or it can become a stand for incense. Compose the parts and in front of you is a charming white Christmas tree, as if powdered with snow. Great festive table decoration and home decor. And you can also make a whole spruce meadow from Christmas tree candles of different sizes and colors. Great, isn't it?",
                            ImageID = aromaconiferous3.ID,
                            CategoryID = aromaConiferous.ID,
                            Price = 2.99m
                        },
                        new Item
                        {
                            Name = "CYPRESS & FIR, PaddyWax",
                            Description = "A candle in a coniferous glass with golden patterns CYPRESS & FIR is packed in a beautiful holiday box. A laconic and elegant gift solution from PaddyWax! Inside - the most New Year's scent: Notes of spruce needles, cypress, pine cones and eucalyptus will instantly fill the house with a feeling of celebration, no matter how the weather is outside the window.",
                            ImageID = aromaconiferous4.ID,
                            CategoryID = aromaConiferous.ID,
                            Price = 3.99m
                        },

                        //ordinary -> big
                        new Item
                        {
                            Name = "BALTIC AMBER, Voluspa",
                            Description = "Exciting warm scent of amber resin, notes of sandalwood, cedar and vanilla flower aftertaste .. BALTIC AMBER is one of the noblest fragrances in the collection. Delicate, sweetish and slightly spicy. The velvety scent of this Voluspa candle is perfect for an evening: cozy and calm.",
                            ImageID = ordinarybig1.ID,
                            CategoryID = ordinaryBig.ID,
                            Price = 2.49m
                        },
                        new Item
                        {
                            Name = "INSIDIAE, Apotheca",
                            Description = "The scent of a pine forest at night: mystical, enveloping and awe-inspiring to the forces of nature. The INSIDIAE candle is made of cedar needles and wood, a lot of air and freshness.\n\nIf you are already in love with this Apotheca scent, order a large candle: it will delight you for 200 hours. Such a candle is perfect for a large apartment or a country house. It has 5 wicks that provide a very even wax melting and a rich aroma.",
                            ImageID = ordinarybig2.ID,
                            CategoryID = ordinaryBig.ID,
                            Price = 3.49m
                        },
                        new Item
                        {
                            Name = "APPLE BLUE CLOVER, Voluspa",
                            Description = "Fresh, crunchy aroma with a subtle fruity apple note. The APPLE BLUE CLOVER candle from Voluspa will appeal to fans of berry and fruit scents as well as to those who love refreshing aromas. This candle seems to smell like mountain air, spring greenery and freedom. Notes of clover, moss and cucumber make the fragrance exciting and light.\n\nThe Voluspa large volume candle is suitable for a country house or a spacious apartment. It contains almost 3.5 kg of wax, so it will burn for 250 hours. This Voluspa candle also has 5 wicks and a stunningly beautiful box.",
                            ImageID = ordinarybig3.ID,
                            CategoryID = ordinaryBig.ID,
                            Price = 4.49m
                        },
                        new Item
                        {
                            Name = "BURNING WOODS, Voluspa",
                            Description = "Fans of smoky fragrances will definitely appreciate the new fragrance from Voluspa. The BURNING WOODS scent will remind you of warm summer evenings by the fire and brighten up any autumn weather! It contains cedar, juniper and smoldering wood. An interesting combination, right?\n\nThe candle in a large glass made of textured white glass with a lid looks amazing! It fits perfectly into a spacious living room or bedroom. The volume of wax is designed for 80 hours of burning. This candle will delight you for a very long time!",
                            ImageID = ordinarybig4.ID,
                            CategoryID = ordinaryBig.ID,
                            Price = 2.39m
                        },

                        //ordinary -> compact
                        new Item
                        {
                            Name = "CATHERINE, Flame Moscow",
                            Description = "The CATHERINE scent is perhaps the coziest of the new Flame Moscow fragrances! The notes of cocoa, cardamom, cinnamon and chocolate beckon! And sage, papyrus and mahogany perfectly set off the scent, adding a fresh note.\n\nThe compact gray glass candle has a wooden wick that will crackle comfortably when burning. This candle is perfect as a small presentation to a colleague or girlfriend!",
                            ImageID = ordinarycompact1.ID,
                            CategoryID = ordinaryCompact.ID,
                            Price = 2.49m
                        },
                        new Item
                        {
                            Name = "FRANCESCO, PaddyWax",
                            Description = "Meet Francesco! This is the name of this candle-person. Good-natured Francesco greets the sunset, lying in a hammock and puffing on a cigar. He knows how to enjoy life like no other. Slowly and tastefully.\n\nThe scent of the FRANCESCO candle is a languid scent of smoke and sweet amber, soft and cozy. It will remind you of an evening by the fireplace or campfire. Each Persona candle by PaddyWax has its own personality. And also a charming hat that can be used as a lid and stand for matches or incense sticks.",
                            ImageID = ordinarycompact2.ID,
                            CategoryID = ordinaryCompact.ID,
                            Price = 2.99m
                        },
                        new Item
                        {
                            Name = "FORBIDDEN FIG, Voluspa",
                            Description = "The FORBIDDEN FIG Candle by Voluspa is a fall fragrance! Figs are just everywhere! It contains fruits and leaves of figs. Rose beautifully and unobtrusively complements the scent.\n\nSmall candle in a carved glass jar with a metal lid: this is always a great idea! Perfect as a small gift for mom or beloved friend. We advise everyone to try the new American brand!",                            
                            ImageID = ordinarycompact3.ID,
                            CategoryID = ordinaryCompact.ID,
                            Price = 3.99m
                        },
                        new Item
                        {
                            Name = "AMBRE LUMIERE NEW, Voluspa",
                            Description = "A woody scent with notes of incense and sweet amber is AMBER LUMIERE by Voluspa. Warm, soft, very cozy and completely versatile, despite the bright components in the composition.\n\nA small candle in a carved glass jar with a metal lid: always a great idea! With her, you can start your acquaintance with one of the most popular fragrances of the Californian brand.",
                            ImageID = ordinarycompact4.ID,
                            CategoryID = ordinaryCompact.ID,
                            Price = 2.99m
                        },
                        
                        //ordinary -> middle
                        new Item
                        {
                            Name = "FRESH BERGAMOT, Collines de Provence",
                            Description = "The scent of the FRESH BERGAMOT candle was created for fans of herbal and citrus aromas. Juicy basil, bitter bergamot and lemon. A warm sandalwood and cedarwood base adds a long-lasting fragrance. And neroli adds sweetness. This is a very beautiful scent that is most closely associated with summer in the Mediterranean.\n\nThe Collines de Provence candle is made of frosted glass and has a ceramic lid that protects the surface of the candle from dust.",
                            ImageID = ordinarymiddle1.ID,
                            CategoryID = ordinaryMiddle.ID,
                            Price = 4.99m
                        },
                        new Item
                        {
                            Name = "FRESH FIG Средняя свеча в стекле, Collines de Provence",
                            Description = "Always appropriate, calm and balanced scent, with a subtle powdery note. If you doubt your choice, choose figs. The scent of the FRESH FIG candle is no exception. Ripe figs and some greenery: perfect! Light the Collines de Provence candle and enjoy the atmosphere at home.",
                            ImageID = ordinarymiddle2.ID,
                            CategoryID = ordinaryMiddle.ID,
                            Price = 4.99m
                        },
                        new Item
                        {
                            Name = "GOJI TAROCCO ORANGE, Voluspa",
                            Description = "GOJI TAROCCO ORANGE Candle is one of the best scents from Voluspa: tart goji berry, ripe mango and sweet and sour red orange. A juicy cocktail that will literally make your mouth water. Exciting, full-bodied, juicy - perfect to start the day: bright and exciting, it will give you a real delight.\n\nThis Voluspa candle is designed to burn for 50 hours. A charming carved glass jar with a glass lid will captivate you at first sight. Just imagine how beautiful the fire will look through the crimson glass!",
                            ImageID = ordinarymiddle3.ID,
                            CategoryID = ordinaryMiddle.ID,
                            Price = 4.99m
                        },
                        new Item
                        {
                            Name = "GOJI TAROCCO ORANGE, Voluspa",
                            Description = "Juicy, sweet aroma! Tart goji berry, ripe mango and sweet and sour red orange. A juicy cocktail that will literally make your mouth water. Exciting, intense, juicy - ideal for starting the day: bright and exciting, it will bring you real delight.\n\nBeautiful and elegant candle in a glass cup and gift box is a ready present for any occasion. And just look at the color of the glass and imagine how beautifully the fire will illuminate it: sheer aesthetic pleasure!",
                            ImageID = ordinarymiddle4.ID,
                            CategoryID = ordinaryMiddle.ID,
                            Price = 2.99m
                        },

                        
                        //ordinary -> set
                        new Item
                        {
                            Name = "JAPONICA ARCHIVE, Voluspa",
                            Description = "Luxurious gift set of 12 Voluspa mini candles. In this set you will find the most popular Japonica fragrances: juicy, sweet, coniferous and spicy. Everything to please the recipient of the gift. And just look at the packaging: the magnificent case will impress the most discerning person. Do you agree?",
                            ImageID = ordinaryset1.ID,
                            CategoryID = ordinarySet.ID,
                            Price = 2.99m
                        },
                        new Item
                        {
                            Name = "NATUR, Skandinavisk",
                            Description = "In the NATUR gift set, Skandinavisk has collected 3 scents inspired by northern nature. FJORD is a light and exciting scent in honor of the fjords, OY is a candle inspired by the islands and HAV is a very natural aroma of the north sea.\n\nThis present will definitely appeal to fans of Scandinavia and wildlife. And also for those who prefer fresh and aquatic scents.",
                            ImageID = ordinaryset2.ID,
                            CategoryID = ordinarySet.ID,
                            Price = 3.39m
                        },
                        new Item
                        {
                            Name = "VANILLA+OAKMOSS & POMEGRANATE+SPRUCE GIFT BOX, PaddyWax",
                            Description = "Knock Knock. This is an evening delivery of Relish pleasure.\nYou will need: 1 bathroom, 2 candles and 20 minutes \nRecipe 1. Vanilla and oakmoss are an unexpected combination, but how pleasant! Fill a warm bathroom, add your favorite foam, pour a glass of champagne and light a VANILLA + OAKMOSS candle. All that remains is to enjoy the moment.\nRecipe 2. The tart-sweet, winey nuances of the POMEGRANATE & SPRUCE candles seem to be meant for romantic evenings. With dim lights, rose petals, foam and everything that makes your head spin so sweetly ...\n\nChoose a fragrance according to your mood or combine them with each other. 2 warm and rich scents in the Relish set are just what you need for a pleasant evening.",
                            ImageID = ordinaryset3.ID,
                            CategoryID = ordinarySet.ID,
                            Price = 4.49m
                        },
                        new Item
                        {
                            Name = "JAPONICA BESTSELLERS PEDESTAL, Voluspa",
                            Description = "JAPONICA BESTSELLERS PEDESTAL 4 is a collection of the most popular Voluspa fragrances this season! Juicy and sweet GOJI TAROCCO ORANGE with notes of goji berries, orange and mango, warm BALTIC AMBER with notes of vanilla, sandalwood, amber, soft woody FRENCH CADE LAVENDER with notes of lavender and juniper. And a very interesting novelty this fall - FORBIDDEN FIG! This is a herbal and fruity aroma of fig leaves and fruits.\n\nThe candles are packed in a luxurious case - a very beautiful and solid gift! You can light the candles individually or create your own special composition. Very convenient, right? We really like it!",
                            ImageID = ordinaryset4.ID,
                            CategoryID = ordinarySet.ID,
                            Price = 4.49m
                        },


                        //decorative -> decorativeChristmas
                        new Item
                        {
                            Name = "Candle, 6 cm, assorted, red / silver / golden, Ball, Christmas",
                            Description = "Size: 6 cm.\nMaterial: paraffin\nDo not ignite near flammable surfaces. Keep out of the reach of children.",
                            ImageID = decorativechristmas4.ID,
                            CategoryID = decorativeChristmas.ID,
                            Price = 2.19m
                        },
                        new Item
                        {
                            Name = "Candle, 14 cm, green, Snow-covered Christmas tree, Christmas bright",
                            Description = "Candle.\nSize: 14 cm.\nMaterial: paraffin.\nKeep out of the reach of children. Do not light near flammable objects. Storage temperature from 0-35 ℃.",
                            ImageID = decorativechristmas2.ID,
                            CategoryID = decorativeChristmas.ID,
                            Price = 2.29m
                        },
                        new Item
                        {
                            Name = "Candle, 12 cm, gray, Pine cone, Christmas",
                            Description = "Candle.\nSize: 12 cm.\nMaterial: paraffin.\nKeep out of the reach of children. Do not light near flammable objects. Storage temperature from 0-35 ℃.",
                            ImageID = decorativechristmas1.ID,
                            CategoryID = decorativeChristmas.ID,
                            Price = 3.79m
                        },                        
                        new Item
                        {
                            Name = "Candle, 13 cm, golden, sparkles, Tiger",
                            Description = "Candle.\nSize: 13 cm.\nMaterial: paraffin.\nDo not light near flammable objects. Keep out of the reach of children. Storage temperature from 0-35 ℃",
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