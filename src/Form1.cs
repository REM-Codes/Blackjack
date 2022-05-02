namespace Blackjack
{
    public partial class Blackjack : Form
    {
        public Blackjack()
        {
            InitializeComponent();
        }

        private enum GameResults
        {
            TIE,
            DEALER,
            PLAYER,
            NULL
        }

        private Card[] deck = new Card[52];
        private readonly Card[] player = new Card[11];
        private readonly Card[] dealer = new Card[11];

        private int playerCardCount = 0;
        private int dealerCardCount = 0;

        private int topCardIndex = 0;

        private GameResults gameResult = GameResults.NULL;

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            topCardIndex = 0;
            playerCardCount = 0;
            dealerCardCount = 0;
            DownCard.Visible = false;
            for(int i = 0; i < 11; i++)
            {
                Array.Clear(player, i, 1);
                Array.Clear(dealer, i, 1);
            }
            GameResult.Visible = false;
            gameResult = GameResults.NULL;
            NewGameButton.Enabled = false;
            HitButton.Enabled = true;
            StayButton.Enabled = true;
            ShuffleCards();
            DealCards();
            UpdateCardDisplay();
        }

        private static string GetCardName(Card card)
        {
            string name = "";
            if (card != null)
            {
                name = card.name;
                switch (card.suit)
                {
                    case Card.Suit.DIAMOND:
                        name += "\u2662";
                        break;
                    case Card.Suit.HEART:
                        name += "\u2661";
                        break;
                    case Card.Suit.CLUB:
                        name += "\u2663";
                        break;
                    case Card.Suit.SPADE:
                        name += "\u2660";
                        break;
                }
            }
            return name;
        }

        private void DealCards()
        {
            player[0] = deck[topCardIndex];
            topCardIndex++;
            playerCardCount++;
            dealer[0] = deck[topCardIndex];
            topCardIndex++;
            dealerCardCount++;
            player[1] = deck[topCardIndex];
            topCardIndex++;
            playerCardCount++;
            dealer[1] = deck[topCardIndex];
            topCardIndex++;
            dealerCardCount++;
        }

        private void DrawCardForPlayer()
        {
            player[playerCardCount] = deck[topCardIndex];
            topCardIndex++;
            UpdateCardDisplay();
            playerCardCount++;
        }

        private void DrawCardForDealer()
        {
            dealer[dealerCardCount] = deck[topCardIndex];
            topCardIndex++;
            UpdateCardDisplay();
            dealerCardCount++;
        }

        private static int FindCardSum(Card[] cards)
        {
            int sum = 0;


            // Add value of all numcards and faces
            foreach (Card card in cards)
            {
                if (card != null && card.value != 1)
                {
                    sum += card.value;
                }
            }

            // choose value for aces
            foreach (Card card in cards)
            {
                if (card != null && card.value == 1)
                {
                    if (sum + 11 > 21)
                    {
                        sum += 1;
                    }
                    else
                    {
                        sum += 11;
                    }
                }
            }

            return sum;
        }

        private void ShuffleCards()
        {
            Random rand = new();

            deck = deck.OrderBy(a => rand.Next()).ToArray();
        }

        private void HitButton_Click(object sender, EventArgs e)
        {
            DrawCardForPlayer();
            if (FindCardSum(player) > 21)
            {
                PlayDealerTurn();
            }
        }

        private void StayButton_Click(object sender, EventArgs e)
        {
            PlayDealerTurn();
        }

        private void PlayDealerTurn()
        {
            HitButton.Enabled = false;
            StayButton.Enabled = false;
            DownCard.Visible = true;
            while (FindCardSum(dealer) < 22)
            {
                if (FindCardSum(dealer) < 17)
                {
                    DrawCardForDealer();
                }
                else { break; }
            }
            CalculateGameEnd();
        }

        private void CalculateGameEnd()
        {
            if (FindCardSum(dealer) > 21 && FindCardSum(player) > 21)
            {
                gameResult = GameResults.DEALER;
            }
            else if (FindCardSum(dealer) > 21)
            {
                gameResult = GameResults.PLAYER;
            }
            else if (FindCardSum(player) > 21)
            {
                gameResult = GameResults.DEALER;
            }
            else if (FindCardSum(player) > FindCardSum(dealer))
            {
                gameResult = GameResults.PLAYER;
            }
            else if (FindCardSum(player) < FindCardSum(dealer))
            {
                gameResult = GameResults.DEALER;
            }
            else
            {
                gameResult = GameResults.TIE;
            }
            switch (gameResult)
            {
                case GameResults.DEALER:
                    GameResult.Text = "GAME RESULT: DEALER WINS";
                    break;
                case GameResults.PLAYER:
                    GameResult.Text = "GAME RESULT: PLAYER WINS";
                    break;
                case GameResults.TIE:
                    GameResult.Text = "GAME RESULT: TIE";
                    break;
            }
            GameResult.Visible = true;
            NewGameButton.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeDeck();
        }

        private void UpdateCardDisplay()
        {
            // DEALER'S CARDS
            UpCard.Text = GetCardName(dealer[0]);
            UpCard.Refresh();
            DownCard.Text = GetCardName(dealer[1]);
            DownCard.Refresh();
            DrawCard1.Text = GetCardName(dealer[2]);
            DrawCard1.Refresh();
            DrawCard2.Text = GetCardName(dealer[3]);
            DrawCard2.Refresh();
            DrawCard3.Text = GetCardName(dealer[4]);
            DrawCard3.Refresh();
            DrawCard4.Text = GetCardName(dealer[5]);
            DrawCard4.Refresh();
            DrawCard5.Text = GetCardName(dealer[6]);
            DrawCard5.Refresh();
            DrawCard6.Text = GetCardName(dealer[7]);
            DrawCard6.Refresh();
            DrawCard7.Text = GetCardName(dealer[8]);
            DrawCard7.Refresh();
            DrawCard8.Text = GetCardName(dealer[9]);
            DrawCard8.Refresh();
            DrawCard9.Text = GetCardName(dealer[10]);
            DrawCard9.Refresh();

            // PLAYER'S CARDS
            Card1.Text = GetCardName(player[0]);
            Card1.Refresh();
            Card2.Text = GetCardName(player[1]);
            Card2.Refresh();
            Card3.Text = GetCardName(player[2]);
            Card3.Refresh();
            Card4.Text = GetCardName(player[3]);
            Card4.Refresh();
            Card5.Text = GetCardName(player[4]);
            Card5.Refresh();
            Card6.Text = GetCardName(player[5]);
            Card6.Refresh();
            Card7.Text = GetCardName(player[6]);
            Card7.Refresh();
            Card8.Text = GetCardName(player[7]);
            Card8.Refresh();
            Card9.Text = GetCardName(player[8]);
            Card9.Refresh();
            Card10.Text = GetCardName(player[9]);
            Card10.Refresh();
            Card11.Text = GetCardName(player[10]);
            Card11.Refresh();
        }

        private void InitializeDeck()
        {
            // SPADES
            deck[0] = new Card(1, "A", Card.Suit.SPADE);
            deck[1] = new Card(2, "2", Card.Suit.SPADE);
            deck[2] = new Card(3, "3", Card.Suit.SPADE);
            deck[3] = new Card(4, "4", Card.Suit.SPADE);
            deck[4] = new Card(5, "5", Card.Suit.SPADE);
            deck[5] = new Card(6, "6", Card.Suit.SPADE);
            deck[6] = new Card(7, "7", Card.Suit.SPADE);
            deck[7] = new Card(8, "8", Card.Suit.SPADE);
            deck[8] = new Card(9, "9", Card.Suit.SPADE);
            deck[9] = new Card(10, "10", Card.Suit.SPADE);
            deck[10] = new Card(11, "J", Card.Suit.SPADE);
            deck[11] = new Card(12, "Q", Card.Suit.SPADE);
            deck[12] = new Card(13, "K", Card.Suit.SPADE);

            // CLUBS
            deck[13] = new Card(1, "A", Card.Suit.CLUB);
            deck[14] = new Card(2, "2", Card.Suit.CLUB);
            deck[15] = new Card(3, "3", Card.Suit.CLUB);
            deck[16] = new Card(4, "4", Card.Suit.CLUB);
            deck[17] = new Card(5, "5", Card.Suit.CLUB);
            deck[18] = new Card(6, "6", Card.Suit.CLUB);
            deck[19] = new Card(7, "7", Card.Suit.CLUB);
            deck[20] = new Card(8, "8", Card.Suit.CLUB);
            deck[21] = new Card(9, "9", Card.Suit.CLUB);
            deck[22] = new Card(10, "10", Card.Suit.CLUB);
            deck[23] = new Card(11, "J", Card.Suit.CLUB);
            deck[24] = new Card(12, "Q", Card.Suit.CLUB);
            deck[25] = new Card(13, "K", Card.Suit.CLUB);

            // HEARTS
            deck[26] = new Card(1, "A", Card.Suit.HEART);
            deck[27] = new Card(2, "2", Card.Suit.HEART);
            deck[28] = new Card(3, "3", Card.Suit.HEART);
            deck[29] = new Card(4, "4", Card.Suit.HEART);
            deck[30] = new Card(5, "5", Card.Suit.HEART);
            deck[31] = new Card(6, "6", Card.Suit.HEART);
            deck[32] = new Card(7, "7", Card.Suit.HEART);
            deck[33] = new Card(8, "8", Card.Suit.HEART);
            deck[34] = new Card(9, "9", Card.Suit.HEART);
            deck[35] = new Card(10, "10", Card.Suit.HEART);
            deck[36] = new Card(11, "J", Card.Suit.HEART);
            deck[37] = new Card(12, "Q", Card.Suit.HEART);
            deck[38] = new Card(13, "K", Card.Suit.HEART);

            // DIAMONDS
            deck[39] = new Card(1, "A", Card.Suit.DIAMOND);
            deck[40] = new Card(2, "2", Card.Suit.DIAMOND);
            deck[41] = new Card(3, "3", Card.Suit.DIAMOND);
            deck[42] = new Card(4, "4", Card.Suit.DIAMOND);
            deck[43] = new Card(5, "5", Card.Suit.DIAMOND);
            deck[44] = new Card(6, "6", Card.Suit.DIAMOND);
            deck[45] = new Card(7, "7", Card.Suit.DIAMOND);
            deck[46] = new Card(8, "8", Card.Suit.DIAMOND);
            deck[47] = new Card(9, "9", Card.Suit.DIAMOND);
            deck[48] = new Card(10, "10", Card.Suit.DIAMOND);
            deck[49] = new Card(11, "J", Card.Suit.DIAMOND);
            deck[50] = new Card(12, "Q", Card.Suit.DIAMOND);
            deck[51] = new Card(13, "K", Card.Suit.DIAMOND);
        }
    }
}