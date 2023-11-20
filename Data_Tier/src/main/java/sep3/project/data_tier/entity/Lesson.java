package sep3.project.data_tier.entity;

import jakarta.persistence.*;
import org.hibernate.annotations.UuidGenerator;


import java.util.HashSet;
import java.util.Set;

@Entity
@Table
public class Lesson {
	@Id
	@Column
	@UuidGenerator
	private String id;
	@Column
	private int date;
	@Column
	private String topic;
	@Column
	private String description;

	@OneToOne
	@JoinColumn(
			name = "homeworkId",
			unique = true,
			nullable = true,
			updatable = true
	)
	private Homework homework;

	@ManyToMany
	@JoinTable(
			name = "attendance",
			joinColumns = @JoinColumn(name = "studentUsername"),
			inverseJoinColumns = @JoinColumn(name = "lessonId")
	)
	private Set<UserEntity> attendance = new HashSet<>();

	public Lesson() {
	}

	public Lesson(int date, String topic, String description) {
		this.date = date;
		this.topic = topic;
		this.description = description;
	}

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public int getDate() {
		return date;
	}

	public void setDate(int date) {
		this.date = date;
	}

	public String getTopic() {
		return topic;
	}

	public void setTopic(String topic) {
		this.topic = topic;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	@Override
	public String toString() {
		return "Lesson{" +
				"id='" + id + '\'' +
				", date=" + date +
				", topic='" + topic + '\'' +
				", description='" + description + '\'' +
				'}';
	}
}
