# GamesStore
ASP.NET MVC University Course Project

&nbsp;&nbsp;&nbsp;&nbsp;**GamesStore** is an online gaming shop application. It is intened for all kinds of gamers - casual gamers, professionals or gaming enthusiasts. The app is developed with .NET 5.

&nbsp;&nbsp;&nbsp;&nbsp;It consists of the following models:
- Game\
&nbsp;&nbsp;&nbsp;&nbsp;Every game has a name, platform, image URL (which associates the game with an image), developer, age rating, description, year of release, price, quantity and date and time of creation and last modification. Games are visible for both authenticated and non-authenticated users.
- Discount code\
&nbsp;&nbsp;&nbsp;&nbsp;Every discount code has the code itself, a discount percentage and date and time of creation and last modification.
- Cart item\
&nbsp;&nbsp;&nbsp;&nbsp;Every cart item constists of an associated game, quantity to be bought, and the total game price equal to the game's price multiplied by the desired quantity.
- Order\
&nbsp;&nbsp;&nbsp;&nbsp;The Every order has an associated user, cart items, discount code (optional), total price, first name, last name, delivery address, phone number and delivery date.
- User\
&nbsp;&nbsp;&nbsp;&nbsp;Every user has an email, first name, last name, username, password, delivery address, role and date and time of creation and last modification. Every user can have one of the following two roles:
    - Admin\
    &nbsp;&nbsp;&nbsp;&nbsp;Admins can make CRUD operations over games, discount codes and users' roles. The first registered user automatically gets the admin role.
    - User\
    &nbsp;&nbsp;&nbsp;&nbsp;Authenticated users can make orders.
</ul>
Both roles can be seeded when making a migration. I have commented the lines for the seed so they don't get added again. Uncommenting them and updating the database will make them appear if they are not present. The app has the functionallity to add more roles if you want to.</br>
&nbsp;

&nbsp;&nbsp;&nbsp;&nbsp;Process of making an order:
1. Open games page
2. Add a desired game to cart by clicking the *Add to cart* button. (User must be authenticated and in the role 'user' to make an order. If he is not authenticated and tries to buy a game, he gets redirected to the login page). After adding a game to cart, the user is redirected to the cart's page.
3. Change the desired game's quantity by changing the number in *Quantity* column and afterwards clicking the *Update* button (optional).
4. Continue shopping by clicking the *Continue shopping* button and repeat steps 2 and 3 while all desired games and their quantities are added to the cart (if you add a game that is already in the cart, its quantity get incremented by one) (optional).
5. Go to the checkout page by clicking the *Checkout* button.
6. Apply a discount code by writing it in the corresponding textbox and clicking the *Apply discount* button (optional). Go to the confirm order details page by clicking the 'Confirm order details' button.
7. By default, the details that user has written in the register form get automatically written in this form. The user can choose wheter to use them or write different ones. After populating the correct data about the delivery, the user must click the *Create order* button to finish the order.
8. The user gets redirected to the finished order page where he can see all the details about the order.