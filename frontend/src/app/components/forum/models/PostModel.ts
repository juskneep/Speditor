import { PostReplyModel } from './PostReplyModel';

export class PostModel {
    public id: string;
    public title: string;
    public content: string;
    public created: Date;
    public userId: string;
    public forumId: string;
    public postReply: PostReplyModel[];
}