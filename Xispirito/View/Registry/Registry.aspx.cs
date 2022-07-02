using System;
using System.Drawing;
using System.Web.UI;
using Xispirito.Controller;
using Xispirito.Models;

namespace Xispirito.View.Registry
{
    public partial class Registry : Page
    {
        private Lecture lecture = new Lecture();
        private LectureBAL lectureBAL = new LectureBAL();

        private UserType userType;

        private AdministratorBAL administratorBAL = new AdministratorBAL();
        private AdministratorLectureBAL administratorLectureBAL = new AdministratorLectureBAL();

        private SpeakerBAL speakerBAL = new SpeakerBAL();
        private SpeakerLectureBAL speakerLectureBAL = new SpeakerLectureBAL();

        private ViewerBAL viewerBAL = new ViewerBAL();
        private ViewerLectureBAL viewerLectureBAL = new ViewerLectureBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["event"] != null)
            {
                lecture.SetId(Convert.ToInt32(Request.QueryString["event"]));
                GetEventInformation(lecture.GetId());

                if (VerifyLectureStatus())
                {
                    if (!IsPostBack)
                    {
                        if (VerifyLectureHasVacancy() == false || userType != UserType.Administrator)
                        {
                            EventSubscribe.Text = "Vagas Esgotadas";
                            EventSubscribe.BackColor = Color.FromArgb(22, 25, 23);
                        }

                        if (User.Identity.IsAuthenticated)
                        {
                            BaseUser baseUser = new BaseUser();
                            baseUser = GetAccount(User.Identity.Name);

                            if (VerifyUserAlreadyRegistered(baseUser))
                            {
                                EventSubscribe.Text = "Cancelar Inscrição";
                                EventSubscribe.BackColor = Color.FromArgb(22, 25, 23);
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/View/Home/Home.aspx");
                }
            }
        }

        private BaseUser GetAccount(string email)
        {
            BaseUser baseUser = new BaseUser();
            if (!viewerBAL.VerifyAccount(email))
            {
                if (!speakerBAL.VerifyAccount(email))
                {
                    if (administratorBAL.VerifyAccount(email))
                    {
                        userType = UserType.Administrator;
                        baseUser = administratorBAL.GetAccount(email);
                    }
                }
                else
                {
                    userType = UserType.Speaker;
                    baseUser = speakerBAL.GetAccount(email);
                }
            }
            else
            {
                userType = UserType.Viewer;
                baseUser = viewerBAL.GetAccount(email);
            }
            return baseUser;
        }

        private bool VerifyLectureHasLimit()
        {
            bool isLectureLimited = false;
            if (lecture.GetLimit() != 0)
            {
                isLectureLimited = true;
            }
            return isLectureLimited;
        }

        private bool VerifyLectureHasVacancy()
        {
            bool hasVacancy = true;
            if (VerifyLectureHasLimit())
            {
                if (viewerLectureBAL.GetLectureRegistrationsNumber(lecture.GetId()) + speakerLectureBAL.GetLectureRegistrationsNumber(lecture.GetId()) >= lecture.GetLimit())
                {
                    hasVacancy = false;
                }
            }
            return hasVacancy;
        }

        private void GetEventInformation(int eventId)
        {
            lecture = lectureBAL.GetLecture(eventId);

            SetEventInformation();
        }

        private void SetEventInformation()
        {
            BackgroundEventImage.ImageUrl = lecture.GetPicture();
            EventImage.ImageUrl = lecture.GetPicture();

            EventTitle.Text = lecture.GetName();

            EventType.Text = lecture.GetModality();
            EventType.BackColor = ModalityColor.GetModalityColor(lecture.GetModality());

            EventAddress.Text = lecture.GetAddress();

            EventTime.Text = lecture.GetDate().ToString("dd/MM/yyyy") + " - As " + lecture.GetDate().ToString("HH:mm") + " (GMT-3) - Duração de " + lecture.GetTime().ToString() + " Minutos";

            EventDescription.Text = lecture.GetDescription();
        }

        protected void Subscribe_Click(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (VerifyLectureStatus())
                {
                    BaseUser baseUser = new BaseUser();
                    baseUser = GetAccount(User.Identity.Name);

                    if (VerifyUserAlreadyRegistered(baseUser))
                    {
                        CancelRegisterToLecture(baseUser);
                    }
                    else
                    {
                        if (VerifyLectureHasVacancy() || userType == UserType.Administrator)
                        {
                            RegisterUserToLecture(baseUser);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Palestra Expirada!", "alert('Você não pode realizar/cancelar Inscrição a Palestras Inativas ou em Andamento!');", true);
                }
            }
            else
            {
                Response.Redirect("~/View/Login/Login.aspx");
            }
        }

        private bool VerifyLectureStatus()
        {
            bool registrationOpen = true;
            if (lecture.IsActive == false || lecture.GetDate() < DateTime.Now)
            {
                registrationOpen = false;
            }
            return registrationOpen;
        }

        private void RegisterUserToLecture(BaseUser objBaseUser)
        {
            if (objBaseUser != null)
            {
                if (userType == UserType.Administrator)
                {
                    _ = new Administrator();
                    Administrator objAdministrator = administratorBAL.GetAccount(User.Identity.Name);

                    AdministratorLecture objAdministratorLecture = new AdministratorLecture(objAdministrator, lecture);
                    administratorLectureBAL.RegisterUserToLecture(objAdministratorLecture);
                }
                else if (userType == UserType.Speaker)
                {
                    _ = new Speaker();
                    Speaker objSpeaker = speakerBAL.GetAccount(User.Identity.Name);

                    SpeakerLecture objSpeakerLecture = new SpeakerLecture(objSpeaker, lecture);
                    speakerLectureBAL.RegisterUserToLecture(objSpeakerLecture);
                }
                else
                {
                    _ = new Viewer();
                    Viewer objViewer = viewerBAL.GetAccount(User.Identity.Name);

                    ViewerLecture objViewerLecture = new ViewerLecture(objViewer, lecture);
                    viewerLectureBAL.RegisterUserToLecture(objViewerLecture);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Usuário Cadastrado com Sucesso!", "alert('Cadastro a Palestra efetuado com sucesso!');", true);
                EventSubscribe.Text = "Cancelar Inscrição";
                EventSubscribe.BackColor = Color.FromArgb(22, 25, 23);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Usuário Inválido!", "alert('Esse tipo de usuário é inválido para efetuar inscrição á palestra!');", true);
            }
        }

        private void CancelRegisterToLecture(BaseUser objBaseUser)
        {
            if (objBaseUser != null)
            {
                if (userType == UserType.Administrator)
                {
                    _ = new Administrator();
                    Administrator objAdministrator = administratorBAL.GetAccount(User.Identity.Name);

                    AdministratorLecture objAdministratorLecture = new AdministratorLecture(objAdministrator, lecture);
                    administratorLectureBAL.DeleteUserSubscription(objAdministratorLecture);
                }
                else if (userType == UserType.Speaker)
                {
                    _ = new Speaker();
                    Speaker objSpeaker = speakerBAL.GetAccount(User.Identity.Name);

                    SpeakerLecture objSpeakerLecture = new SpeakerLecture(objSpeaker, lecture);
                    speakerLectureBAL.DeleteUserSubscription(objSpeakerLecture);
                }
                else
                {
                    _ = new Viewer();
                    Viewer objViewer = viewerBAL.GetAccount(User.Identity.Name);

                    ViewerLecture objViewerLecture = new ViewerLecture(objViewer, lecture);
                    viewerLectureBAL.DeleteUserSubscription(objViewerLecture);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inscrição Cancelada!", "alert('Sua Inscrição a Palestra foi Cancelada!');", true);
                EventSubscribe.Text = "Inscrever-se";
                EventSubscribe.BackColor = Color.FromArgb(112, 22, 14);
            }   
        }

        private bool VerifyUserAlreadyRegistered(BaseUser objBaseUser)
        {
            bool userAlreadyRegistered = false;
            if (objBaseUser != null)
            {
                if (userType == UserType.Administrator)
                {
                    _ = new Administrator();
                    Administrator objAdministrator = administratorBAL.GetAccount(User.Identity.Name);

                    AdministratorLecture objAdministratorLecture = new AdministratorLecture(objAdministrator, lecture);
                    userAlreadyRegistered = administratorLectureBAL.VerifyUserAlreadyRegistered(objAdministratorLecture);
                }
                else if (userType == UserType.Speaker)
                {
                    _ = new Speaker();
                    Speaker objSpeaker = speakerBAL.GetAccount(User.Identity.Name);

                    SpeakerLecture objSpeakerLecture = new SpeakerLecture(objSpeaker, lecture);
                    userAlreadyRegistered = speakerLectureBAL.VerifyUserAlreadyRegistered(objSpeakerLecture);
                }
                else
                {
                    _ = new Viewer();
                    Viewer objViewer = viewerBAL.GetAccount(User.Identity.Name);

                    ViewerLecture objViewerLecture = new ViewerLecture(objViewer, lecture);
                    userAlreadyRegistered = viewerLectureBAL.VerifyUserAlreadyRegistered(objViewerLecture);
                }
            }
            return userAlreadyRegistered;
        }
    }
}