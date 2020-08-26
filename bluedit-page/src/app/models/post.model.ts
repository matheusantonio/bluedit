import { Reply } from "./reply.model";

export interface Post{
    id : string
    title : string
    tags : string[]
    subForum : string
    author : string
    content : string
    replies : Reply[]
    time : Date
    upvotes : number
}

export interface CreatePost {
    title : string
    content : string
}