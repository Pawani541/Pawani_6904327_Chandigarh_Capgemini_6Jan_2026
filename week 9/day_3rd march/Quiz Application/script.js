const questions = [
    {
        question: "What does HTML stand for?",
        options: [
            "Hyper Text Markup Language",
            "High Tech Modern Language",
            "Hyper Transfer Machine Language",
            "Home Tool Markup Language"
        ],
        correct: 0
    },
    {
        question: "Which language is used for styling?",
        options: [
            "HTML",
            "Python",
            "CSS",
            "C++"
        ],
        correct: 2
    },
    {
        question: "Which is used for logic in web?",
        options: [
            "Java",
            "JavaScript",
            "C#",
            "SQL"
        ],
        correct: 1
    }
];

let currentQuestion = 0;
let score = 0;
let timeLeft = 10;
let timer;

function loadQuestion() {
    const q = questions[currentQuestion];
    document.getElementById("question").innerText = q.question;

    const buttons = document.querySelectorAll(".option-btn");
    buttons.forEach((btn, index) => {
        btn.innerText = q.options[index];
        btn.classList.remove("correct", "wrong");
    });

    timeLeft = 10;
    document.getElementById("timer").innerText = "Time Left: " + timeLeft + "s";
    startTimer();
}

function selectAnswer(index) {
    clearInterval(timer);
    const correctIndex = questions[currentQuestion].correct;
    const buttons = document.querySelectorAll(".option-btn");

    if (index === correctIndex) {
        buttons[index].classList.add("correct");
        score++;
    } else {
        buttons[index].classList.add("wrong");
        buttons[correctIndex].classList.add("correct");
    }
}

function nextQuestion() {
    currentQuestion++;
    if (currentQuestion < questions.length) {
        loadQuestion();
    } else {
        document.querySelector(".quiz-container").innerHTML =
            "<h2>Your Final Score: " + score + "/" + questions.length + "</h2>";
    }
}

function startTimer() {
    timer = setInterval(() => {
        timeLeft--;
        document.getElementById("timer").innerText =
            "Time Left: " + timeLeft + "s";

        if (timeLeft <= 0) {
            clearInterval(timer);
            nextQuestion();
        }
    }, 1000);
}

loadQuestion();