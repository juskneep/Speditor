import { PostModel } from './PostModel';

export class ForumModel {
  public id: string;
  public title: string;
  public description: string;
  public created: Date;
  public imageUrl: string;
  public posts: PostModel[];
}
