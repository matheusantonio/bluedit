export interface Reply{
    id : string
    author : string
    content : string
    replies : Reply[]
    time : Date
    upvotes : number
    userVote : boolean
}

export interface NewReply{
    author : string
    content : string
    postId : string
}