import {Comment} from "./comment";

export interface Article {
  id: string;
  title: string;
  description: string;
  estimatedMinutesToRead: string;
  tags: string[];
  imageUrl: string | undefined;
  thumbsUp: number;
  thumbsDown: number;
  comments: Comment[];
  author: string;
  content: any[];
  createdDate: Date;
  modifiedDate: Date;
}
