using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Classi;

namespace WebApplication1.Controllers
{
    [Route("articoli")]
    [ApiController]
    public class ArticoliController : ControllerBase
    {
        // qua dentro dobbiamo programmare le chiamate
        // che potrebbero essere decine...
        // come prima chiamata faremo un - GET - che restituisce
        // tutti gli articoli

        [Route("listaarticoli")]
        [HttpGet]
        public List<Articolo> GetListArticoli()
        {
            SqlConnection mySqlConnection = null;

            // dichiaro una List di Articoli
            List<Articolo> myListArticoli = new List<Articolo>();

            try
            {
                // per connettermi al db
                // mi serve una stringa di connessione
                String stringaConnessione = "Server=localhost\\SQLEXPRESS; Database=DBGestionaleVI; Trusted_Connection=True";

                // creo l'istanza della connessione al DB
                mySqlConnection = new SqlConnection(stringaConnessione);

                // provo aprire la connessione al DB
                mySqlConnection.Open();

                // creo l'oggetto che mi permette di estrarre i dati da TArticoli
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = "SELECT * FROM TArticoli";

                // devo eseguire il mySqlCommand e ottengo un SqlDataReader
                SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                // adesso il mySqlDataReader contiene i vari record
                // che andranno copiati dentro myListArticoli

                while (mySqlDataReader.Read())
                {
                    // leggo un record alla volta
                    // se sono qua significa che ho trovato un Record in TArticoli
                    //Articolo myArticolo = new Articolo();
                    //// compilo myArticolo e poi l'ho aggiungo alla myListArticoli
                    //myArticolo.ArticoloID = Convert.ToInt32(mySqlDataReader["ArticoloID"]);
                    //myArticolo.Nome = Convert.ToString(mySqlDataReader["Nome"]);
                    //myArticolo.Descrizione = Convert.ToString(mySqlDataReader["Descrizione"]);
                    //myArticolo.PrezzoUnitarioVendita = Convert.ToSingle(mySqlDataReader["PrezzoUnitarioVendita"]);
                    //myArticolo.Giacenza = Convert.ToInt32(mySqlDataReader["Giacenza"]);
                    //myArticolo.AliquotaIVA = Convert.ToInt32(mySqlDataReader["AliquotaIVA"]);

                    Articolo myArticolo = new Articolo
                    {
                        ArticoloID = Convert.ToInt32(mySqlDataReader["ArticoloID"]),
                        Nome = Convert.ToString(mySqlDataReader["Nome"]),
                        Descrizione = Convert.ToString(mySqlDataReader["Descrizione"]),
                        PrezzoUnitarioVendita = Convert.ToSingle(mySqlDataReader["PrezzoUnitarioVendita"]),
                        Giacenza = Convert.ToInt32(mySqlDataReader["Giacenza"]),
                        AliquotaIVA = Convert.ToInt32(mySqlDataReader["AliquotaIVA"])
                    };

                    // myArticolo puo essere aggiunto alla lista degli articoli
                    myListArticoli.Add(myArticolo);
                }

            }
            catch (Exception exc)
            {
                // in caso di errore dovremo comunicare al frontend
                // tramite un'altro metodo
            }
            finally
            {
                // che tutto sia OK oppure KO il codice dentro finally verra sempre eseguita x.y
                // chiudo la connessione al db
                mySqlConnection.Close();
            }

            // fine della funzione GetListArticoli

            return myListArticoli;
        }

        //********************************************************
        //          RETURN ARTICOLI CON GIACENZA ZERO
        //********************************************************


        [Route("listaarticoli_fuoristock")]
        [HttpGet]
        public List<Articolo> GetListArticoliFuoriStock()
        {
            SqlConnection mySqlConnection = null;

            // dichiaro una List di Articoli
            List<Articolo> myListArticoli = new List<Articolo>();

            try
            {
                // per connettermi al db
                // mi serve una stringa di connessione
                String stringaConnessione = "Server=localhost\\SQLEXPRESS; Database=DBGestionaleVI; Trusted_Connection=True";

                // creo l'istanza della connessione al DB
                mySqlConnection = new SqlConnection(stringaConnessione);

                // provo aprire la connessione al DB
                mySqlConnection.Open();

                // creo l'oggetto che mi permette di estrarre i dati da TArticoli
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = "SELECT * FROM TArticoli WHERE Giacenza=0";

                // devo eseguire il mySqlCommand e ottengo un SqlDataReader
                SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                // adesso il mySqlDataReader contiene i vari record
                // che andranno copiati dentro myListArticoli

                while (mySqlDataReader.Read())
                {
                    Articolo myArticolo = new Articolo
                    {
                        ArticoloID = Convert.ToInt32(mySqlDataReader["ArticoloID"]),
                        Nome = Convert.ToString(mySqlDataReader["Nome"]),
                        Descrizione = Convert.ToString(mySqlDataReader["Descrizione"]),
                        PrezzoUnitarioVendita = Convert.ToSingle(mySqlDataReader["PrezzoUnitarioVendita"]),
                        Giacenza = Convert.ToInt32(mySqlDataReader["Giacenza"]),
                        AliquotaIVA = Convert.ToInt32(mySqlDataReader["AliquotaIVA"])
                    };

                    // myArticolo puo essere aggiunto alla lista degli articoli
                    myListArticoli.Add(myArticolo);
                }

            }
            catch (Exception exc)
            {
                // in caso di errore dovremo comunicare al frontend
                // tramite un'altro metodo
            }
            finally
            {
                // che tutto sia OK oppure KO il codice dentro finally verra sempre eseguita x.y
                // chiudo la connessione al db
                mySqlConnection.Close();
            }

            // fine della funzione GetListArticoli

            return myListArticoli;
        }


        //*************************************************
        //    RESTITUISCE
        //*************************************************
        [Route("articolo")]
        [HttpGet]
        public Articolo GetArticolo(Int32 ArticoloID)
        {
            // tutto il codice che legge 1 solo articolo
            // utilizziamo 1 parametro
            // SELECT * TArticoli WHERE ArticoloID=@ArticoloID

            SqlConnection mySqlConnection = null;

            // dichiaro una List di Articoli

            Articolo mArticolo = new Articolo();

            try
            {
                // per connettermi al db
                // mi serve una stringa di connessione
                String stringaConnessione = "Server=localhost\\SQLEXPRESS; Database=DBGestionaleVI; Trusted_Connection=True";

                // creo l'istanza della connessione al DB
                mySqlConnection = new SqlConnection(stringaConnessione);

                // provo aprire la connessione al DB
                mySqlConnection.Open();

                // creo l'oggetto che mi permette di estrarre i dati da TArticoli
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;

                mySqlCommand.Parameters.Add("@ID", SqlDbType.Int);
                mySqlCommand.Parameters["@ID"].Value = ArticoloID;

                mySqlCommand.CommandText = $"SELECT * FROM TArticoli WHERE ArticoloID=@ID";

                // devo eseguire il mySqlCommand e ottengo un SqlDataReader
                SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                // adesso il mySqlDataReader contiene i vari record
                // che andranno copiati dentro myListArticoli
                
                if (mySqlDataReader.Read())
                {
                    mArticolo.ArticoloID = Convert.ToInt32(mySqlDataReader["ArticoloID"]);
                    mArticolo.Nome = Convert.ToString(mySqlDataReader["Nome"]);
                    mArticolo.Descrizione = Convert.ToString(mySqlDataReader["Descrizione"]);
                    mArticolo.PrezzoUnitarioVendita = Convert.ToSingle(mySqlDataReader["PrezzoUnitarioVendita"]);
                    mArticolo.Giacenza = Convert.ToInt32(mySqlDataReader["Giacenza"]);
                    mArticolo.AliquotaIVA = Convert.ToInt32(mySqlDataReader["AliquotaIVA"]);   
                }

            }
            catch (Exception exc)
            {
                // in caso di errore dovremo comunicare al frontend
                // tramite un'altro metodo
            }
            finally
            {
                // che tutto sia OK oppure KO il codice dentro finally verra sempre eseguita x.y
                // chiudo la connessione al db
                mySqlConnection.Close();
            }

            // fine della funzione GetListArticoli
            return mArticolo;
            // return di myArticolo
        }

        //**********************************************************
        //   RESTITUISCE L'ARTICOLO CHE CONTIENE LA PAROLA CHIAVE
        //**********************************************************

        [Route("articolitext")]
        [HttpGet]
        public List<Articolo> GetArticoliByText(String Testo)
        {
            SqlConnection mySqlConnection = null;

            // dichiaro una List di Articoli
            List<Articolo> viewArticoli = new List<Articolo>();

            try
            {
                // per connettermi al db
                // mi serve una stringa di connessione
                String stringaConnessione = "Server=localhost\\SQLEXPRESS; Database=DBGestionaleVI; Trusted_Connection=True";

                // creo l'istanza della connessione al DB
                mySqlConnection = new SqlConnection(stringaConnessione);

                // provo aprire la connessione al DB
                mySqlConnection.Open();

                // creo l'oggetto che mi permette di estrarre i dati da TArticoli
                SqlCommand mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;

                mySqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar);
                mySqlCommand.Parameters["@Text"].Value = "%" + Testo + "%";

                mySqlCommand.CommandText = "SELECT * FROM TArticoli WHERE Descrizione LIKE @Text OR Nome LIKE @Text";

                // devo eseguire il mySqlCommand e ottengo un SqlDataReader
                SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    Articolo myArticolo = new Articolo();

                    myArticolo.ArticoloID = Convert.ToInt32(mySqlDataReader["ArticoloID"]);
                    myArticolo.Nome = Convert.ToString(mySqlDataReader["Nome"]);
                    myArticolo.Descrizione = Convert.ToString(mySqlDataReader["Descrizione"]);
                    myArticolo.PrezzoUnitarioVendita = Convert.ToSingle(mySqlDataReader["PrezzoUnitarioVendita"]);
                    myArticolo.Giacenza = Convert.ToInt32(mySqlDataReader["Giacenza"]);
                    myArticolo.AliquotaIVA = Convert.ToInt32(mySqlDataReader["AliquotaIVA"]);

                    viewArticoli.Add(myArticolo);
                }

                mySqlDataReader.Close();

                
            }
            catch (Exception exc)
            {
               
            }
            finally
            {
                // che tutto sia OK oppure KO il codice dentro finally verra sempre eseguita x.y
                // chiudo la connessione al db
                mySqlConnection.Close();
            }

            return viewArticoli;
        }

        //*****************************************************************************
        //      INSERISCI UN ARTICOLO NELLA LISTA
        //*****************************************************************************

        [Route("inserisciarticolo")]
        [HttpPost]
        public String PostInserisciArticolo(Articolo myArticolo)
        {
            // codice che fa insert di articolo in TArticolo
            // se tutto va bene -> return "OK"
            // se c'e eccezione -> return exc.Message;
            SqlConnection mySqlConnection = null;
            try
            {
                String stringaConnessione = "Server=localhost\\SQLEXPRESS; Database=DBGestionaleVI; Trusted_Connection=True";

                // creo l'istanza della connessione al DB
                mySqlConnection = new SqlConnection(stringaConnessione);

                // provo aprire la connessione al DB
                mySqlConnection.Open();

                SqlCommand mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;

                mySqlCommand.Parameters.Add("@Nome", SqlDbType.NVarChar);
                mySqlCommand.Parameters["@Nome"].Value = myArticolo.Nome;

                mySqlCommand.Parameters.Add("@Descrizione", SqlDbType.NVarChar);
                mySqlCommand.Parameters["@Descrizione"].Value = myArticolo.Descrizione;

                mySqlCommand.Parameters.Add("@PrezzoUnitarioVendita", SqlDbType.Float);
                mySqlCommand.Parameters["@PrezzoUnitarioVendita"].Value = myArticolo.PrezzoUnitarioVendita;

                mySqlCommand.Parameters.Add("@Giacenza", SqlDbType.Int);
                mySqlCommand.Parameters["@Giacenza"].Value = myArticolo.Giacenza;

                mySqlCommand.Parameters.Add("@AliquotaIVA", SqlDbType.Int);
                mySqlCommand.Parameters["@AliquotaIVA"].Value = myArticolo.AliquotaIVA;

                mySqlCommand.CommandText = "INSERT INTO TArticoli (Nome, Descrizione, PrezzoUnitarioVendita, Giacenza, AliquotaIVA) " +
                                           "VALUES (@Nome, @Descrizione, @PrezzoUnitarioVendita, @Giacenza, @AliquotaIVA)";

                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                mySqlConnection.Close();
            }

            return "OK";
        }

        //*****************************************************************************
        //      ELIMINA UN ARTICOLO DALLA TABELA
        //*****************************************************************************

        [Route("articoli/{id}")]
        [HttpDelete]

        public String EliminaArticolo(Int32 id)
        {
            SqlConnection mySqlConnection = null;

            try {
                String stringaConnessione = "Server=localhost\\SQLEXPRESS; Database=DBGestionaleVI; Trusted_Connection=True";
                mySqlConnection = new SqlConnection(stringaConnessione);

                mySqlConnection.Open();

                SqlCommand mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;

                // devo assicurarmi che ID e sicuro
                mySqlCommand.Parameters.Add("@ID", SqlDbType.Int);
                mySqlCommand.Parameters["@ID"].Value = id;

                mySqlCommand.CommandText = "DELETE FROM TArticoli WHERE ArticoloID=@ID";

                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return "Eccezione" + ex.Message;
            }
            finally
            {
                mySqlConnection.Close();
            }

            return "OK";
        }

        //*******************************************************************
        //     MODIFICA LA GIACENZA IN BASE ALLA VENDITA O AL ACQUISTO
        //*******************************************************************

        [Route("aggiornagiacenza")]
        [HttpPut]

        public String PutAggiornaGiacenza (Int32 ArticoloID, Int32 Quantita, String Tipo)
        {
            SqlConnection mySqlConnection = null;
            try
            {
                //-----------------------------------------------------------------------------------------------------------------
                
                String stringaConnessione = "Server=localhost\\SQLEXPRESS; Database=DBGestionaleVI; Trusted_Connection=True";
                mySqlConnection = new SqlConnection(stringaConnessione);

                mySqlConnection.Open();

                //------------------------------------------------------------------------------------------------------------------

                SqlCommand mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;

                mySqlCommand.Parameters.Add("@idArticolo", SqlDbType.Int);
                mySqlCommand.Parameters["@idArticolo"].Value = ArticoloID;

                mySqlCommand.Parameters.Add("@quantita", SqlDbType.Int);
                mySqlCommand.Parameters["@quantita"].Value = Quantita;

                Int32 quantita = Convert.ToInt32(mySqlCommand.Parameters["@quantita"].Value);

                mySqlCommand.Parameters.Add("@tipo", SqlDbType.NVarChar);
                mySqlCommand.Parameters["@tipo"].Value = Tipo;

                // tipo di operazione da fare [acquisto o vendita]
                String operazione = Convert.ToString(mySqlCommand.Parameters["@tipo"].Value);

                // selecteaza tot articolul unde id-ul este cel cerut de noi: id, descriere, giacenza, quantita, aliquota ...
                mySqlCommand.CommandText = "SELECT * FROM TArticoli WHERE ArticoloID=@idArticolo";

                SqlDataReader myDataReader = mySqlCommand.ExecuteReader();

                if (myDataReader.Read())
                {
                    Int32 giacenzaValue = Convert.ToInt32(myDataReader["Giacenza"]);

                    if (operazione == "acquisto")
                    {
                        mySqlCommand.CommandText = $"UPDATE TArticoli SET Giacenza=Giacenza+@quantita WHERE ArticoloID=@idArticolo";
                    }
                    else if (operazione == "vendita")
                    {
                        if (giacenzaValue - quantita >= 0)
                        {
                            mySqlCommand.CommandText = $"UPDATE TArticoli SET Giacenza=Giacenza-@quantita WHERE ArticoloID=@idArticolo";
                        } 
                        else
                        {
                            // chiude data reader in caso che non è possibile modifica la giacenza
                            myDataReader.Close();
                            return "Articoli non sufficienti";
                        }   
                    }
                }

                // chiudo il reader
                myDataReader.Close();

                mySqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return "KO " + ex.Message;
            }
            finally
            {
                if (mySqlConnection != null)
                    mySqlConnection.Close();
            }

            return "Operazione eseguita con successo";
        }

    }
}
