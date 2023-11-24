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
	private HomeworkEntity homework;

	@OneToOne
	@JoinColumn
			(
					name = "feedbackId"
			)
	private FeedbackEntity feedback;

	@Column
	private String answer;

	public HandInHomework() {
	}

	public HandInHomework(String id, UserEntity user, HomeworkEntity homework, String answer) {
		this.id = id;
		this.user = user;
		this.homework = homework;
		this.answer = answer;
	}

	public HandInHomework(String id, UserEntity user, HomeworkEntity homework, FeedbackEntity feedback, String answer) {
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

	public HomeworkEntity getHomework() {
		return homework;
	}

	public void setHomework(HomeworkEntity homework) {
		this.homework = homework;
	}

	public FeedbackEntity getFeedback() {
		return feedback;
	}

	public void setFeedback(FeedbackEntity feedback) {
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

