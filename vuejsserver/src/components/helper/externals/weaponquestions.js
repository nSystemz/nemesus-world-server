//Weapon Questions
var questionList = [];

var question = new Object();
question.text = "Dürfen Waffen öffentlich getragen werden?";
question.answertext = "Ja|Nein|Alle Waffen sind verboten|Nur Pistolen dürfen öffentlich getragen werden";
question.answer = 2;
questionList.push(question);

question = new Object();
question.text = "Welcher von den vier vorliegenden Fällen ist erlaubt?";
question.answertext = "Ich schiess wild in meinem Haus rum|Eine Gang bedroht mit den Waffen Zivilisten|Hendrik schießt auf einem freien Gelände gegen Pfandflaschen|Paul geht seinem Hobby in der Schießbude nach";
question.answer = 4;
questionList.push(question);

question = new Object();
question.text = "Umgang mit einer Schusswaffe hat...";
question.answertext = "wer damit schießt|wer die Waffe verbringt oder mitnimmt|wer die Waffe unbrauchbar macht|alles ist richtig";
question.answer = 4;
questionList.push(question);

question = new Object();
question.text = "Welche Gegenstände zählen zu den verbotenen Waffen?";
question.answertext = "Wasserpistolen|Feuerwaffen mit Dauerfeuereinrich-tung|Softairs|Schusswaffen mit Schalldämpfer";
question.answer = 2;
questionList.push(question);

question = new Object();
question.text = "Wo ist der Kauf von legalen Waffen möglich?";
question.answertext = "Im Ammunation|Von meinem Freund|von Privatpersonen|Vom Schwarzmarkt";
question.answer = 1;
questionList.push(question);

question = new Object();
question.text = "Wer ist Erwerber einer Schusswaffe im Sinne des Waffengesetzes?";
question.answertext = "Der Sohn des Käufers|Derjenige, der in einem Waffengeschäft lediglich den Kaufvertrag für eine Waffe unterschreibt|Der Dieb, der die Waffe klaut|Keiner";
question.answer = 3;
questionList.push(question);

question = new Object();
question.text = "Wann „erwirbt“ der Käufer eine Waffe im Sinne des Waffengesetzes?";
question.answertext = "Beim anschauen von Waffen im Ammunation|Beim Bezahlvergang|Sobald man einen Ammunation betretet|Bei der Waffenübergabe";
question.answer = 4;
questionList.push(question);

question = new Object();
question.text = "Wie dürfen Waffen transportiert werden?";
question.answertext = "Im Hosenbund|Im abgeschlossenen Kofferraum/Mittelkonsole|Auf der Rückbank im Auto|Im Rucksack";
question.answer = 2;
questionList.push(question);

question = new Object();
question.text = "Wie alt muss man sein, um den Waffenschein erhalten zu können?";
question.answertext = "16|21|18|30";
question.answer = 3;
questionList.push(question);

question = new Object();
question.text = "Fallen selbstgebaute Schusswaffen unter das Waffengesetz?";
question.answertext = "Ja|Nein|Dazu gibt es keine Regelung|Die selbstgebauten Waffen sind ungefährlich";
question.answer = 1;
questionList.push(question);

question = new Object();
question.text = "An wen dürfen erlaubnispflichtige Pistolen verkauft werden?";
question.answertext = "An Polizeibeamte|An jeden|An Freunde|Nur an Personen mit gültigem Waffenschein";
question.answer = 4;
questionList.push(question);

question = new Object();
question.text = "Benötigt man zum jagen einen Waffenschein?";
question.answertext = "Nein|Für Jäger gilt eine Sondererlaubnis|Ja|Das jagen ist verboten";
question.answer = 2;
questionList.push(question);

export default questionList;

