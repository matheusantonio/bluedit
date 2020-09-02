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
    userVote : boolean
}

export interface CreatePost {
    title : string
    content : string
    tags : string[]
}