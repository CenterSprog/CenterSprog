package sep3.project.data_tier.entity;

import jakarta.persistence.*;
import org.hibernate.annotations.UuidGenerator;

@Entity
@Table
public class HandInHomework {
	@Id
	@Column
	@UuidGenerator
	private String id;
	@OneToOne
	@JoinColumn
			(
					name = "studentUsername"
			)
	private UserEntity user;
	@OneToOne
	@JoinColumn
		(
				name = "homeworkId"
		)
	private Homework homework;

	@OneToOne
	@JoinColumn
			(
					name = "feedbackId"
			)
	private Feedback feedback;

	@Column
	private String answer;

	public HandInHomework() {
	}

	public HandInHomework(String id, UserEntity user, Homework homework, String answer) {
		this.id = id;
		this.user = user;
		this.homework = homework;
		this.answer = answer;
	}

	public HandInHomework(String id, UserEntity user, Homework homework, Feedback feedback, String answer) {
		this.id = id;
		this.user = user;
		this.homework = homework;
		this.feedback = feedback;
		this.answer = answer;
	}

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public UserEntity getUser() {
		return user;
	}

	public void setUser(UserEntity user) {
		this.user = user;
	}

	public Homework getHomework() {
		return homework;
	}

	public void setHomework(Homework homework) {
		this.homework = homework;
	}

	public Feedback getFeedback() {
		return feedback;
	}

	public void setFeedback(Feedback feedback) {
		this.feedback = feedback;
	}

	public String getAnswer() {
		return answer;
	}

	public void setAnswer(String answer) {
		this.answer = answer;
	}

	@Override
	public String toString() {
		return "HandInHomework{" +
				"id='" + id + '\'' +
				", user=" + user +
				", homework=" + homework +
				", feedback=" + feedback +
				", answer='" + answer + '\'' +
				'}';
	}
}

