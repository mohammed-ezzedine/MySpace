export interface Comment {
  id: string;
  thumbsUp: number;
  thumbsDown: number;
  comments: Comment[] | undefined;
  author: string;
  content: string;
  createdDate: Date;
  modifiedDate: Date;
}
