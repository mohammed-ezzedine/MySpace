import {Tag} from "./tag";
import {Comment} from "./comment";

export interface Article {
  id: string;
  title: string;
  description: string;
  estimatedMinutesToRead: string;
  tags: Tag[];
  imageUrl: string | undefined;
  thumbsUp: number;
  thumbsDown: number;
  comments: Comment[];
  author: string;
  content: string;
  createdDate: Date;
  modifiedDate: Date;
}
